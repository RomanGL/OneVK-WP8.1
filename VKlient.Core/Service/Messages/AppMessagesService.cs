using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Model.Message;
using Microsoft.Practices.ServiceLocation;
using OneVK.Core;
using OneVK.Request;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Core.Messages;
using OneVK.Service.Common;
using OneVK.Enums.Message;
using OneVK.Enums.App;
using OneVK.ViewModel.Messages;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис приложения для обработки сообщений.
    /// </summary>
    public class AppMessagesService : IAppMessagesService
    {
        private const int MAX_ATTEMPTS_NUMBER = 3;

        IMessagesCacheService messagesCacheService;
        IMessagesUsersService messagesUsersService;
        IDialogsCacheService dialogsService;
        VKLongPollService longPollService;

        /// <summary>
        /// Возвращает или задает идентификатор текущей открытой беседы.
        /// </summary>
        public long CurrentConversationID { get; set; }

        /// <summary>
        /// Запустить сервис сообщений и восстановить состояние.
        /// </summary>
        public void StartAndRestore()
        {
            messagesCacheService = ServiceLocator.Current.GetInstance<IMessagesCacheService>();
            messagesUsersService = ServiceLocator.Current.GetInstance<IMessagesUsersService>();
            dialogsService = ServiceLocator.Current.GetInstance<IDialogsCacheService>();
            longPollService = ServiceLocator.Current.GetInstance<VKLongPollService>();

            longPollService.MessagesReceived += LongPollService_MessagesReceived;
        }

        /// <summary>
        /// Остановить сервис сообщений и сохранить состояние.
        /// </summary>
        public void StopAndSave()
        {
            longPollService.MessagesReceived -= LongPollService_MessagesReceived;
            messagesCacheService = null;
            messagesUsersService = null;
            dialogsService = null;
            longPollService = null;
        }

        /// <summary>
        /// Вызывается при получении новых сообщений через LongPoll.
        /// </summary>
        private async void LongPollService_MessagesReceived(object sender, MessagesReceivedEventArgs e)
        {
            var messagesIDs = from info in e.Messages
                              select info.MessageID;

            List<VKMessage> messages;
            try { messages = await GetMessagesForIDs(messagesIDs); }
            catch (AuthorizationRequiredException) { return; }

            var conversations = new Dictionary<long, List<VKMessage>>();
            foreach (VKMessage msg in messages)
            {
                long key = msg.IsChatMessage ? -msg.ChatID : (long)msg.UserID;

                if (conversations.ContainsKey(key)) conversations[key].Add(msg);
                else conversations[key] = new List<VKMessage> { msg }; 
            }

            foreach (long convID in conversations.Keys)
            {
                var msgs = conversations[convID];
                try { await messagesUsersService.SetUsers(msgs); }
                catch (AuthorizationRequiredException) { return; }
                catch (CacheConversationException ex)
                {
                    ServiceLocator.Current.GetInstance<ILogService>().Log(ex);
                    continue;
                }

                IConversation conv = null;
                try { conv = await messagesCacheService.GetConversation(convID); }
                catch (CacheConversationException ex)
                {
                    ServiceLocator.Current.GetInstance<ILogService>().Log(ex);
                    continue;
                }

                foreach (var msg in msgs)
                {
                    SendPush(conv, msg);
                    if (msg.Type == VKMessageType.Sent && conv.Messages.NewSentMessages.Count > 0)
                    {
                        var sent = conv.Messages.NewSentMessages.FirstOrDefault(m =>
                            m.ID == msg.ID);

                        if (sent == null)
                        {
                            conv.Messages.Insert(0, msg);
                            continue;
                        }

                        int index = conv.Messages.IndexOf(sent);
                        conv.Messages.RemoveAt(index);
                        conv.Messages.Insert(index, msg);

                        conv.Messages.NewSentMessages.Remove(sent);
                    }
                    else conv.Messages.Insert(0, msg);
                }

                try { await messagesCacheService.CacheConversation(conv); }
                catch (Exception ex) { ServiceLocator.Current.GetInstance<ILogService>().Log(ex); }
            }
        }

        /// <summary>
        /// Возвращает коллекцию сообщений по их идентификаторам.
        /// </summary>
        /// <param name="ids">Коллекция идентификаторов.</param>
        /// <exception cref="AuthorizationRequiredException"/>
        /// <exception cref="InvalidOperationException"/>
        private async Task<List<VKMessage>> GetMessagesForIDs(IEnumerable<ulong> ids)
        {
            var request = new GetByIDRequest
            {
                MessageIDs = ids.ToList()
            };

            for (int i = 0; i < MAX_ATTEMPTS_NUMBER; i++)
            {
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.AccessDenied || response.Error.ErrorType == VKErrors.AuthorizationFailed)
                    throw new AuthorizationRequiredException("Запрос провалился из-за необходимости выполнить авторизацию.");
                if (response.Error.ErrorType == VKErrors.None)
                    return response.Response.Items;

                if (i + 1 < MAX_ATTEMPTS_NUMBER)
                    await Task.Delay(1000);
                continue;                
            }

            throw new InvalidOperationException("Не удалось получить коллекцию сообщений по их идентификаторам.");
        }

        /// <summary>
        /// Отправляет Push-уведомление внутри приложения.
        /// </summary>
        /// <param name="conversation">Беседа, уведомление для которой нужно отправить.</param>
        /// <param name="message">Сообщение для отправки.</param>
        private void SendPush(IConversation conversation, VKMessage message)
        {
            if ((message.Type & VKMessageType.Sent) == VKMessageType.Sent) return;

            var navParameter = new NavigateToPageMessage
            {
                Page = AppViews.ConversationView,
                Parameter = conversation.ID
            };

            string title = null;
            if (message.IsChatMessage)
                title = String.Format("{0}\n{1}", message.Sender.FullName, conversation.Title);
            else
                title = message.Sender.FullName;

            CoreHelper.SendInAppPush(message.Body,
                    title, PopupMessageType.Default,
                    message.Sender.Photo50, parameter: navParameter);
        }
    }
}
