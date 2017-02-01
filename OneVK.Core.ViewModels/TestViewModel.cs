using Microsoft.Practices.Prism.StoreApps;
using OneVK.Core.Models.AppNotifications;
using OneVK.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Newtonsoft.Json;
using OneVK.Core.VK.Models.LongPoll;
using OneVK.Core.VK;

namespace OneVK.Core.ViewModels
{
    /// <summary>
    /// Представляет модель представления тестовой страницы.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class TestViewModel : ViewModelBase
    {
        private const string SIMPLE_PUSH_DEFAULT_TITLE = "Обычное уведомление";
        private const string SIMPLE_PUSH_DEFAULT_CONTENT = "Это обычное тестовое уведомление";
        private const string SIMPLE_PUSH_ERROR_TITLE = "Уведомление об ошибке";
        private const string SIMPLE_PUSH_ERROR_CONTENT = "Это тестовое уведомление об ошибке OneVK";
        private const string SIMPLE_PUSH_WARNING_TITLE = "Уведомление предупреждение";
        private const string SIMPLE_PUSH_WARNING_CONTENT = "Это тестовое уведомления типа предупреждения";
        private const string SIMPLE_PUSH_INFO_TITLE = "Информативное уведомление";
        private const string SIMPLE_PUSH_INFO_CONTENT = "Это тестовое информативное уведомление";
        private const string ENABLE_PUSH_NOTIFICATIONS = "EnablePushNotifications";

        private IAppNotificationsService appNotificationsService;
        private IVKLongPollService vkLongPollService;
        private IGrooveMusicService gms;
        private IPushNotificationsService pushNotificationsService;
        private ISettingsService settingsService;

        public TestViewModel(IAppNotificationsService appNotificationsService, IVKLongPollService vkLongPollService, 
            IGrooveMusicService gms, IPushNotificationsService pushNotificationsService, ISettingsService settingsService)
        {
            this.appNotificationsService = appNotificationsService;
            this.vkLongPollService = vkLongPollService;
            this.gms = gms;
            this.pushNotificationsService = pushNotificationsService;
            this.settingsService = settingsService;

            ShowSimplePush = new DelegateCommand<string>(OnShowSimplePush);
            ShowCustomPush = new DelegateCommand(OnShowCustomPush);
            DeserializeLongPollResponse = new DelegateCommand(OnDeserializeLongPollResponse);
        }

        /// <summary>
        /// Состояние сервиса мгновенных сообщений.
        /// </summary>
        public bool LongPollServiceState
        {
            get { return vkLongPollService.IsWorking; }
            set
            {
                if (vkLongPollService.IsWorking) vkLongPollService.Stop();
                else vkLongPollService.Start();
            }
        }
        /// <summary>
        /// Текст настраиваемого уведомления.
        /// </summary>
        public string CustomPushContent { get; set; }
        /// <summary>
        /// Заголовок настраиваемого уведомления.
        /// </summary>
        public string CustomPushTitle { get; set; }
        /// <summary>
        /// Тип настраиваемого уведомления.
        /// </summary>
        public int CustomPushType { get; set; }
        /// <summary>
        /// Представление навигации настраиваемого уведомления.
        /// </summary>
        public int CustomPushView { get; set; }
        /// <summary>
        /// Выключить звук для настраиваемого уведомления.
        /// </summary>
        public bool CustomPushNoSound { get; set; }
        /// <summary>
        /// Выключить вибрацию для настраиваемого уведомления.
        /// </summary>
        public bool CustomPushNoVibration { get; set; }
        /// <summary>
        /// Длительность настраиваемого уведомления.
        /// </summary>
        public string CustomPushDuration { get; set; } = "6";
        /// <summary>
        /// Включены ли Push-уведомления.
        /// </summary>
        public bool IsPushEnabled
        {
            get { return settingsService.Get(ENABLE_PUSH_NOTIFICATIONS, false); }
            set
            {
                settingsService.Set(ENABLE_PUSH_NOTIFICATIONS, value);
                ChangePushState(value);
            }
        }
        /// <summary>
        /// Выполняется изменение состояния Push-сервиса.
        /// </summary>
        public bool IsPushStateChanging { get; private set; }

        /// <summary>
        /// Команда отображения простого уведомления.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand<string> ShowSimplePush { get; private set; }
        /// <summary>
        /// Команда отображения настраиваемого уведомления.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand ShowCustomPush { get; private set; }
        /// <summary>
        /// Команда десериализации тестового ответа LongPoll.
        /// </summary>
        [DoNotNotify]
        public DelegateCommand DeserializeLongPollResponse { get; private set; }

        /// <summary>
        /// Вызывается при навигации на страницу.
        /// </summary>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            vkLongPollService.ServiceStarted += VKLongPollService_ServiceStarted;
            vkLongPollService.ServiceStopped += VKLongPollService_ServiceStopped;

            base.OnNavigatedTo(e, viewModelState);
        }

        /// <summary>
        /// Вызывается при навигации со страницы.
        /// </summary>
        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            if (!suspending)
            {
                vkLongPollService.ServiceStarted -= VKLongPollService_ServiceStarted;
                vkLongPollService.ServiceStopped -= VKLongPollService_ServiceStopped;
            }

            base.OnNavigatingFrom(e, viewModelState, suspending);
        }

        private void VKLongPollService_ServiceStopped(IVKLongPollService sender, VKLongPollServiceStopReason args)
        {
            OnPropertyChanged(nameof(LongPollServiceState));
        }

        private void VKLongPollService_ServiceStarted(IVKLongPollService sender, EventArgs args)
        {
            OnPropertyChanged(nameof(LongPollServiceState));
        }

        private void OnShowSimplePush(string type)
        {
            var notification = new AppNotification { Duration = TimeSpan.FromSeconds(10) };
            switch (type)
            {
                case "Warning":
                    notification.Title = SIMPLE_PUSH_WARNING_TITLE;
                    notification.Content = SIMPLE_PUSH_WARNING_CONTENT;
                    notification.Type = AppNotificationType.Warning;
                    break;
                case "Error":
                    notification.Title = SIMPLE_PUSH_ERROR_TITLE;
                    notification.Content = SIMPLE_PUSH_ERROR_CONTENT;
                    notification.Type = AppNotificationType.Error;
                    break;
                case "Info":
                    notification.Title = SIMPLE_PUSH_INFO_TITLE;
                    notification.Content = SIMPLE_PUSH_INFO_CONTENT;
                    notification.Type = AppNotificationType.Info;
                    break;
                default:
                    notification.Title = SIMPLE_PUSH_DEFAULT_TITLE;
                    notification.Content = SIMPLE_PUSH_DEFAULT_CONTENT;
                    break;
            }

            appNotificationsService.SendNotification(notification);
        }

        private void OnShowCustomPush()
        {
            var notification = new AppNotification
            {
                Title = CustomPushTitle,
                Content = CustomPushContent,
                NoSound = CustomPushNoSound,
                NoVibration = CustomPushNoVibration,
                Type = (AppNotificationType)CustomPushType
            };

            int duration = 6;
            if (int.TryParse(CustomPushDuration, out duration))
                notification.Duration = TimeSpan.FromSeconds(duration);

            switch (CustomPushView)
            {
                case 1:
                    notification.DestinationView = "ErrorView";
                    notification.NavigationParameter = JsonConvert.SerializeObject(new Tuple<string, string>(
                        "Уведомление",
                        "Вы попали сюда через настраиваемое тестовое уведомление. Нажмите кнопку Назад, чтобы вернуться."));
                    break;
                case 2:
                    notification.DestinationView = "SettingsView";
                    break;
                case 3:
                    notification.DestinationView = "NewsView";
                    break;
                case 4:
                    notification.DestinationView = "AboutView";
                    break;
            }

            appNotificationsService.SendNotification(notification);
        }

        private void OnDeserializeLongPollResponse()
        {
            //const string json = "{ \"ts\": 196851352, \"updates\": [ [ 0, 11835293, 0 ], [ 1, 11835293, 144 ] ] } ";
            //var update = JsonConvert.DeserializeObject<VKLongPollResponse>(json);

            //var s = await gms.GetArtistImageURL("lady gaga");
        }
        
        private async void ChangePushState(bool isOn)
        {
            IsPushStateChanging = true;

            byte currentRetry = 0;
            while (currentRetry < 4)
            {
                try
                {
                    if (isOn) await pushNotificationsService.RegisterDevice();
                    else await pushNotificationsService.UnregisterDevice();

                    settingsService.Set(ENABLE_PUSH_NOTIFICATIONS, isOn);
                    break;
                }
                catch (Exception ex)
                {
                    if (++currentRetry < 4)
                    {
                        int timeout = currentRetry * 5;
                        var notification = new AppNotification
                        {
                            Type = AppNotificationType.Warning,
                            Title = ex.Message,
                            Content = $"Повтор через {timeout} секунд",
                            Duration = TimeSpan.FromSeconds(timeout)
                        };
                        appNotificationsService.SendNotification(notification);
                        await Task.Delay(timeout * 1000);
                    }
                    else
                    {
                        settingsService.Set(ENABLE_PUSH_NOTIFICATIONS, !isOn);
                        var notification = new AppNotification
                        {
                            Type = AppNotificationType.Error,
                            Title = $"Не удалось {(isOn ? "включить" : "отключить")} Push-уведомления",
                            Content = "Возвращены предыдущие настройки"
                        };
                        appNotificationsService.SendNotification(notification);
                    }
                }             
            }

            IsPushStateChanging = false;
        }
    }
}
