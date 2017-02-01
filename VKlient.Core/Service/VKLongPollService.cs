using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OneVK.Enums.Common;
using OneVK.Enums.LongPoll;
using OneVK.Model.LongPoll;
using OneVK.Helpers;
using OneVK.Response;
using OneVK.Request;
using System.Diagnostics;
using OneVK.Core;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Enums.App;
using OneVK.Model.Message;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис для работы с LongPoll-сервером ВКонтакте.
    /// </summary>
    public class VKLongPollService
    {
        private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(15) };
        private CancellationTokenSource _cts;
        private VKLongPollServerData _serverData;
        private bool _isWorking;
        private byte _numberOfRetries;

        #region События
        /// <summary>
        /// Происходит при получении новых сообщений через LongPoll.
        /// </summary>
        public event EventHandler<MessagesReceivedEventArgs> MessagesReceived = delegate{ };
        /// <summary>
        /// Происходит при остановке LongPoll-сервиса ВКонтакте.
        /// </summary>
        public event EventHandler<LongPollStoppedEventArgs> ServiceStopped = delegate { };
        /// <summary>
        /// Происходит при изменении счетчика непрочитанных сообщений.
        /// </summary>
        public event EventHandler<MessagesCounterChangedEventArgs> MessagesCounterChanged = delegate { };
        /// <summary>
        /// Пользователь начал набирать текст в чате или диалоге.
        /// </summary>
        public event EventHandler<UserIsTypingEventArgs> UserIsTyping = delegate { };
        #endregion

        /// <summary>
        /// Выполняется ли в данный момент работа с LongPoll-сервером.
        /// </summary>
        public bool IsWorking 
        { 
            get { return _isWorking; }
            private set { _isWorking = value; }
        }

        /// <summary>
        /// Запускает процесс работы с LongPoll-сервером ВКонтакте.
        /// </summary>
        public async void StartLongPolling(bool trace = false)
        {
            if (IsWorking) return;
            IsWorking = true;
#if DEBUG
            Debug.WriteLine("LongPoll service started.");
#endif        
            _timer.Tick += Timer_Tick;
            _timer.Start();
            CheckCounters();

            _cts = new CancellationTokenSource();            

            try
            {
                if (_serverData == null)
                {
                    try
                    {
                        if (!await GetServerData(trace))
                            return;
                    }
                    catch (Exception) { }                 
                }

                if (trace)
                {
                    CoreHelper.SendInAppPush(
                        "Сервис мгновенных сообщений готов",
                        "Сервис успешно запущен", PopupMessageType.Info);
                }

                while (IsWorking)
                {
#if DEBUG
                    Debug.WriteLine("LongPoll request sent. Waiting for data...");
#endif   
                    var response = await VKHelper.GetLongPollResponse(_serverData.Server, _serverData.Key, 
                        _serverData.Ts, 2, _cts.Token);

                    if (response.Error == VKLongPollErrors.ConnectionError)
                    {
                        if (_numberOfRetries == 5)
                        {
#if DEBUG
                            Debug.WriteLine("LongPoll connection failed. Stopping the service...");
#endif 
                            Stop(LongPollStopReason.ConnectionError);
                            return;
                        }
                        _numberOfRetries++;
#if DEBUG
                        Debug.WriteLine(String.Format("LongPoll connecton error. Attempt number: {0}. Attempt after {1} seconds.",
                            _numberOfRetries, 5 * _numberOfRetries));
#endif    
                        CoreHelper.SendInAppPush(
                            "Повтор через " + 5 * _numberOfRetries + " секунд",
                            "Ошибка соединения", PopupMessageType.Warning);

                        await Task.Delay(TimeSpan.FromSeconds(5 * _numberOfRetries), _cts.Token);
                        continue;
                    }
                    else if (response.Error == VKLongPollErrors.DataIsOutdated)
                    {
                        _serverData.Ts = response.NewTs;
                        await Task.Delay(500);
                        continue;
                    }
                    else if (response.Error == VKLongPollErrors.DataIsCorrupted ||
                        response.Error == VKLongPollErrors.KeyIsExpired)
                        if (!await GetServerData()) return;

#if DEBUG
                    Debug.WriteLine("LongPoll data received.");
#endif    

                    _numberOfRetries = 0;
                    _serverData.Ts = response.NewTs;
                    var messages = new List<MessageInfo>();

                    for (int i = 0; i < response.Updates.Count; i++)
                    {
                        switch (response.Updates[i].Type)
                        {
                            case VKLongPollUpdateType.MessageDeleted:
                                break;
                            case VKLongPollUpdateType.MessageFlagsReplaced:
                                break;
                            case VKLongPollUpdateType.MessageFlagsSetted:
                                break;
                            case VKLongPollUpdateType.MessageFlagsResetted:
                                break;
                            case VKLongPollUpdateType.NewMessage:
                                messages.Add((MessageInfo)response.Updates[i].GetInfo());
                                break;
                            case VKLongPollUpdateType.ReceivedMessagesReaded:
                                break;
                            case VKLongPollUpdateType.SentMessagesReaded:
                                break;
                            case VKLongPollUpdateType.UserOnline:
                                break;
                            case VKLongPollUpdateType.UserOffline:
                                break;
                            case VKLongPollUpdateType.ChatParametersChanged:
                                break;
                            case VKLongPollUpdateType.UserIsTypingInDialog:
                                UserIsTyping(this, new UserIsTypingEventArgs((UserIsTypingInfo)response.Updates[i].GetInfo()));
                                break;
                            case VKLongPollUpdateType.UserIsTypingInChat:
                                UserIsTyping(this, new UserIsTypingEventArgs((UserIsTypingInfo)response.Updates[i].GetInfo()));
                                break;
                            case VKLongPollUpdateType.UserMakesACall:
                                break;
                            case VKLongPollUpdateType.MessageCounterChanged:
                                MessagesCounterChanged(this, new MessagesCounterChangedEventArgs(
                                    ((MessagesCounterInfo)response.Updates[i].GetInfo()).Count));
                                break;
                        }    
                    }

                    OnMessagesReceived(messages);

                    await Task.Delay(500);
                }
            }
            catch (OperationCanceledException) { }
        }        

        /// <summary>
        /// Останавливает работу с LongPoll-сервером ВКонтакте.
        /// </summary>
        public void StopLongPolling()
        {
            Stop(LongPollStopReason.ByUser);
        } 

        /// <summary>
        /// Останавливает сервис работы с LongPoll-Сервисом ВКонтакте.
        /// </summary>
        /// <param name="reason">Причина остановки сервиса.</param>
        private void Stop(LongPollStopReason reason)
        {
            if (!IsWorking) return;
            IsWorking = false;
            _numberOfRetries = 0;
            _timer.Stop();
            _timer.Tick -= Timer_Tick;

            if (_cts != null)
            {
                _cts.Cancel(true);
            }
            _serverData = null;

            OnServiceStopped(reason);    
        }
       
        /// <summary>
        /// Попытаться загрузить данные для подключения к LongPoll-Сервису ВКонтакте.
        /// Возвращает успешность операции.
        /// </summary>
        private async Task<bool> GetServerData(bool trace = false)
        {
#if DEBUG
            Debug.WriteLine("Getting LongPoll Server Data...");
#endif    
            if (trace)
            {
                CoreHelper.SendInAppPush(
                    "Получение данных для подключения к серверу...",
                    "Выполняется подключение", PopupMessageType.Info);
            }

            var response = await(new GetLongPollServerRequest { NeedPts = VKBoolean.True }).ExecuteAsync(_cts.Token);

            if (response.Error.ErrorType == VKErrors.None)
            {
                _serverData = response.Response;
                return true;
            }
            else
            {
                if (_numberOfRetries == 5)
                {
#if DEBUG
                    Debug.WriteLine("Getting of LongPoll server data failed. Stopping the service...");
#endif 
                    if (trace)
                    {
                        CoreHelper.SendInAppPush(
                                    "Не удалось получить данные для подключения к серверу",
                                    "Сервис мгновенных сообщений", PopupMessageType.Error);
                    }

                    Stop(LongPollStopReason.CantGetServerData);
                    return false;
                }
                _numberOfRetries++;
#if DEBUG
                Debug.WriteLine(String.Format("LongPoll connecton error. Attempt number: {0}. Attempt after {1} seconds.",
                    _numberOfRetries, 5 * _numberOfRetries));
#endif    
                if (trace)
                {
                    CoreHelper.SendInAppPush(
                            "Повтор через " + 5 * _numberOfRetries + " секунд",
                            "Ошибка соединения", PopupMessageType.Warning);
                }

                await Task.Delay(TimeSpan.FromSeconds(5 * _numberOfRetries));
                return await GetServerData(trace);
            }
        }

        /// <summary>
        /// Вызывается при остановке сервиса LongPoll.
        /// </summary>
        /// <param name="reason">Причина остановки сервиса.</param>
        private void OnServiceStopped(LongPollStopReason reason)
        {
            ServiceStopped(this, new LongPollStoppedEventArgs(reason));
#if DEBUG
            Debug.WriteLine("LongPoll service stopped. Reason: " + reason.ToString());
#endif        

            CoreHelper.SendInAppPush(
                        "Коснитесь для перезапуска",
                        "Сервис мгновенных сообщений остановлен", PopupMessageType.Error,
                        actionToDo: new Action(() => StartLongPolling(true)));
        }

        /// <summary>
        /// Вызывается при получении новых сообщений через LongPoll.
        /// </summary>
        /// <param name="messages">Список полученных сообщений.</param>
        public void OnMessagesReceived(List<MessageInfo> messages)
        {
            if (messages.Count == 0)
                return;

            //var msgs = new Dictionary<long, List<MessageInfo>>();

            //for (int i = 0; i < messages.Count; i++)
            //{
            //    long convID;
            //    if (messages[i].IsChatMessage) convID = -messages[i].ChatID;
            //    else convID = (long)messages[i].UserID;

            //    if (!msgs.ContainsKey(convID))
            //        msgs[convID] = new List<MessageInfo>();
            //    var list = msgs[convID];

            //    list.Add(messages[i]);
            //}

            MessagesReceived(this, new MessagesReceivedEventArgs(messages));
        }

        /// <summary>
        /// Вызывается при срабатывании таймера.
        /// </summary>
        private static void Timer_Tick(object sender, object e)
        {
            CheckCounters();
        }

        /// <summary>
        /// Проверить счетчики.
        /// </summary>
        private static async void CheckCounters()
        {
            var response = await (new GetCountersRequest()).ExecuteAsync();
            if (response.Error.ErrorType != VKErrors.None) return;

            Messenger.Default.Send(response.Response);
        }
    }
}
