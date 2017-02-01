using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Core.VK.Models.LongPoll;
using Windows.Foundation;
using Windows.Web.Http;
using System.Threading;
using OneVK.Core.Services;
using OneVK.Core.Models.AppNotifications;
using OneVK.Core.VK.Models.Common;
using Newtonsoft.Json;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Представляет сервис для работы с LongPoll-подключением к ВКонтакте.
    /// </summary>
    public sealed class VKLongPollService : IVKLongPollService
    {
        private const string LONGPOLL_CONNECTION_MASK = "http://{0}?act=a_check&key={1}&ts={2}&wait={3}&mode={4}";
        private const byte LONGPOLL_MODE = 2;
        private const byte LONGPOLL_WAIT = 25;
        private const byte MAX_RETRIES_NUMBER = 5;

        private readonly object lockObject = new object();
        private CancellationTokenSource cts;

        private IAppNotificationsService appNotificationsService;
        private IVKService vkService;

        /// <summary>
        /// Просиходит при запуске сервиса.
        /// </summary>
        public event TypedEventHandler<IVKLongPollService, EventArgs> ServiceStarted;
        /// <summary>
        /// Происходит при остановке сервиса.
        /// </summary>
        public event TypedEventHandler<IVKLongPollService, VKLongPollServiceStopReason> ServiceStopped;
        /// <summary>
        /// Происходит при получении новых обновлений.
        /// </summary>
        public event TypedEventHandler<IVKLongPollService, IReadOnlyList<IVKLongPollUpdate>> UpdatesReceived;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VKLongPollService"/>.
        /// </summary>        
        /// <param name="appNotificationsService">Сервис внутренних уведомлений.</param>
        /// <param name="vkService">Сервис работы с ВКонтакте.</param>
        public VKLongPollService(IAppNotificationsService appNotificationsService, IVKService vkService)
        {
            this.appNotificationsService = appNotificationsService;
            this.vkService = vkService;
        }

        /// <summary>
        /// Режим отладки. Выводятся подробные данные о состоянии сервиса.
        /// </summary>
        public bool DebugMode { get; set; }
        /// <summary>
        /// Работает ли в данный момент сервис.
        /// </summary>
        public bool IsWorking { get; private set; }

        /// <summary>
        /// Запустить LongPoll сервис.
        /// </summary>
        public async void Start()
        {
            if (IsWorking) return;

            try
            {
                IsWorking = true;
                cts = new CancellationTokenSource();
                if (ServiceStarted != null)
                    ServiceStarted(this, EventArgs.Empty);
                
                await ProcessLongPoll();

                var notification = new AppNotification
                {
                    Type = AppNotificationType.Info,
                    Title = "Сервис мгновенных сообщений успешно запущен",
                    Content = "Теперь вы будете в курсе всех новых сообщений",
                    Duration = TimeSpan.FromSeconds(6)
                };
                appNotificationsService.SendNotification(notification);
            }
            catch (OperationCanceledException)
            {
                OnServiceStopped(VKLongPollServiceStopReason.ByUser);
            }
            catch (WebConnectionException)
            {
                OnServiceStopped(VKLongPollServiceStopReason.ConnectionError);
            }
            catch (Exception)
            {
                OnServiceStopped(VKLongPollServiceStopReason.InternalError);
                throw;
            }
        }

        /// <summary>
        /// Остановить LongPoll сервис.
        /// </summary>
        public void Stop()
        {
            if (!IsWorking) return;
            
            cts.Cancel();
            cts = null;               
        }

        /// <summary>
        /// Запускает процесс работы LongPoll.
        /// </summary>
        private async Task ProcessLongPoll()
        {
            var serverData = await GetServerData();
            
            appNotificationsService.SendNotification(new AppNotification
            {
                Type = AppNotificationType.Info,
                Title = "Сервис мгновенных сообщений успешно запущен",
                Content = "Теперь вы будете в курсе всех новых сообщений",
                Duration = TimeSpan.FromSeconds(6)
            });

            while (serverData != null)
            {
                var response = await GetResponse(serverData);

                if (response.Error == VKLongPollErrors.None)
                {
                    serverData.Ts = response.Ts;
                    OnUpdatesReceived(response);
                }
                else if (response.Error == VKLongPollErrors.DataIsOutdated)
                {
                    serverData.Ts = response.Ts;
                }
                else if (response.Error == VKLongPollErrors.DataIsCorrupted ||
                        response.Error == VKLongPollErrors.KeyIsExpired)
                {
                    serverData = await GetServerData();
                }
            }

            throw new Exception("Exit from main LongPoll loop");
        }

        /// <summary>
        /// Возвращает данные для подключения к LongPoll серверу.
        /// </summary>
        private async Task<VKLongPollServerData> GetServerData()
        {
            appNotificationsService.SendNotification(new AppNotification
            {
                Type = AppNotificationType.Default,
                Content = "Получение данных для подключения к серверу..."
            });

            byte currentRetry = 0;
            while (currentRetry < MAX_RETRIES_NUMBER)
            {
                var request = new Request<VKLongPollServerData>("messages.getLongPollServer", token: cts.Token);
                var response = await vkService.ExecuteRequestAsync(request);

                if (response.IsSuccess) return response.Response;                
                else if (response.Error == VKErrors.ConnectionError)
                {
                    if (++currentRetry < MAX_RETRIES_NUMBER)
                    {
                        int timeout = currentRetry * 5;
                        SendConnectionErrorPush(timeout);
                        await Task.Delay(timeout * 1000, cts.Token);
                        continue;
                    }
                }
            }

            var notification = new AppNotification
            {
                Type = AppNotificationType.Error,
                Title = "Не удалось получить данные для подключения",
                Content = "Сервис мгновенных сообщений будет остановлен"
            };
            appNotificationsService.SendNotification(notification);

            throw new Exception("Exit from GetServerData loop");
        }

        /// <summary>
        /// Вызывается при остановке сервиса.
        /// </summary>
        /// <param name="reason">Причина остановки.</param>
        private void OnServiceStopped(VKLongPollServiceStopReason reason)
        {
            IsWorking = false;
            if (ServiceStopped != null) ServiceStopped(this, reason);

            var notification = new AppNotification
            {
                Type = AppNotificationType.Error,
                Title = "Сервис мгновенных сообщений остановлен",
                Content = "Коснитесь для перезапуска",
                ActionToDo = Start,
                Duration = TimeSpan.FromSeconds(15)
            };
            appNotificationsService.SendNotification(notification);
        }

        /// <summary>
        /// Вызывается при получении новых обновлений.
        /// </summary>
        /// <param name="response">Данные ответа LongPoll сервера.</param>
        private void OnUpdatesReceived(VKLongPollResponse response)
        {
            if (UpdatesReceived != null)
                UpdatesReceived(this, response.Updates);

            var notification = new AppNotification
            {
                Type = AppNotificationType.Info,
                Title = "Полученые новые обновления",
                Content = "Сервис мгновенных собщений",
            };
            appNotificationsService.SendNotification(notification);
        }

        /// <summary>
        /// Отправить уведомление об ошибке соединения с таймаутом.
        /// </summary>
        /// <param name="secondsTimeout"></param>
        private void SendConnectionErrorPush(int secondsTimeout)
        {
            var notification = new AppNotification
            {
                Type = AppNotificationType.Warning,
                Title = "Ошибка соединения",
                Content = $"Повтор через {secondsTimeout} секунд",
                Duration = TimeSpan.FromSeconds(secondsTimeout)
            };
            appNotificationsService.SendNotification(notification);
        }

        /// <summary>
        /// Выполнить запрос к LongPoll серверу ВКонтакте
        /// </summary>
        /// <param name="data">Данные для подключения.</param>
        private async Task<VKLongPollResponse> GetResponse(VKLongPollServerData data)
        {
            using (var client = new HttpClient())
            {
                byte currentRetry = 0;
                while (currentRetry < MAX_RETRIES_NUMBER)
                {
                    HttpResponseMessage response = null;
                    try
                    {
                        response = await client.GetAsync(new Uri(String.Format(
                            LONGPOLL_CONNECTION_MASK, data.Server, data.Key, data.Ts,
                            LONGPOLL_WAIT, LONGPOLL_MODE))).AsTask(cts.Token);
                    }
                    catch (OperationCanceledException) { throw; }
                    catch (Exception)
                    {
                        if (++currentRetry < MAX_RETRIES_NUMBER)
                        {
                            int timeout = currentRetry * 5;
                            SendConnectionErrorPush(timeout);
                            await Task.Delay(timeout * 1000, cts.Token);
                            continue;
                        }
                    }

                    string json = await response.Content.ReadAsStringAsync().AsTask(cts.Token);
                    return JsonConvert.DeserializeObject<VKLongPollResponse>(json);
                }
            }

            throw new Exception("Exit from GetResponse loop");
        }
    }
}
