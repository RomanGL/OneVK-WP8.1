using Microsoft.Practices.ServiceLocation;
using OneVK.Core;
using OneVK.Core.Collections;
using OneVK.Core.Messages;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Helpers;
using OneVK.Model.Message;
using OneVK.Model.Profile;
using OneVK.Request;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис для работы с пользователями сообщений.
    /// </summary>
    internal sealed class MessagesUserService : IMessagesUsersService
    {
        private const int MAX_ATTEMPTS_NUMBER = 5;

        IConversationsService conversationsService;
        IMessagesCacheService cacheService;

        /// <summary>
        /// Возвращает пользователей указанной беседы.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        public async Task<IEnumerable<VKProfileShort>> GetConversationUsers(long convID)
        {
            if (conversationsService == null) conversationsService = ServiceLocator.Current.GetInstance<IConversationsService>();
            if (cacheService == null) cacheService = ServiceLocator.Current.GetInstance<IMessagesCacheService>();

            IConversation conversation = null;
            try { conversation = await cacheService.GetConversation(convID); }
            catch (Exception) { }

            if (conversation == null)
            {
                if (convID < 0) conversation = await conversationsService.GetChat((uint)-convID);
                else conversation = await conversationsService.GetDialog((ulong)convID);
            }

            if (convID < 0) return ((IChat)conversation).Users;
            else return new List<VKProfileShort> { ((IDialog)conversation).User };
        }

        /// <summary>
        /// Устанавливает пользователей для коллекции сообщений.
        /// </summary>
        /// <param name="messages">Коллекция сообщений.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        public async Task SetUsers(IEnumerable<VKMessage> messages)
        {
            if (conversationsService == null) conversationsService = ServiceLocator.Current.GetInstance<IConversationsService>();
            if (cacheService == null) cacheService = ServiceLocator.Current.GetInstance<IMessagesCacheService>();

            var firstMsg = messages.First();
            if (!messages.All(m => m.ChatID == firstMsg.ChatID || m.UserID == firstMsg.UserID))
                throw new ArgumentException("Все сообщения должны принадлежать одному чату или одному отправителю.");

            if (firstMsg.IsChatMessage) await WorkOnChat(firstMsg.ChatID, messages);
            else await WorkOnDialog(firstMsg.UserID, messages);
        }

        /// <summary>
        /// Обработать пользователей чата.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        /// <param name="messages">Коллекция сообщений.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        private async Task WorkOnChat(uint chatID, IEnumerable<VKMessage> messages)
        {
            bool isUpdated = false;
            IChat chat = null;

            try { chat = await cacheService.GetConversation(-chatID) as IChat; }
            catch (Exception) { }

            if (chat == null)
            {
                chat = await conversationsService.GetChat(chatID);
                isUpdated = true;
            }

            foreach (var msg in messages)
                msg.Sender = chat.Users.FirstOrDefault(u => u.ID == msg.UserID);

            IEnumerable<VKMessage> actionMessages = from msg in messages
                                                    where msg.Action != VKChatMessageActionType.None && msg.ActionMid > 0
                                                    select msg;
            IEnumerable<VKMessage> unknownSenders = null;

            if (!isUpdated && !messages.All(m => m.Sender != null))
            {
                chat = await conversationsService.GetChat(chatID);
                foreach (var msg in messages)
                    msg.Sender = chat.Users.FirstOrDefault(u => u.ID == msg.UserID);

                unknownSenders = from msg in messages
                                 where msg.Sender == null
                                 select msg;
            }

            var usersIDs = new List<ulong>();

            if (actionMessages.Count() > 0)
                usersIDs.AddRange(actionMessages.Select(m => (ulong)m.ActionMid));
            if (unknownSenders != null && unknownSenders.Count() > 0)
                usersIDs.AddRange(unknownSenders.Select(m => m.UserID).Distinct());

            if (usersIDs.Count == 0) return;

            var users = await GetUsers(usersIDs);
            if (users == null || users.Count == 0)
                throw new CacheConversationException("Не удалось загрузить информацию о некоторых пользователях.");

            foreach (var msg in actionMessages)
                msg.ActionUser = users.FirstOrDefault(u => u.ID == (ulong)msg.ActionMid);

            if (unknownSenders != null)
            {
                foreach (var msg in unknownSenders)
                    msg.Sender = users.FirstOrDefault(u => u.ID == msg.UserID);
            }
        }

        /// <summary>
        /// Обработать пользователей диалога.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="messages">Коллекция сообщений.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        private async Task WorkOnDialog(ulong userID, IEnumerable<VKMessage> messages)
        {
            IDialog dialog = null;
            try { dialog = await cacheService.GetConversation((long)userID) as IDialog; }
            catch (Exception) { }

            if (dialog == null) dialog = await conversationsService.GetDialog(userID);

            foreach (var msg in messages)
                msg.Sender = dialog.User;
        }

        /// <summary>
        /// Возвращает коллекцию пользователей с указанными идентификаторами.
        /// </summary>
        /// <param name="usersIDs">Коллекция идентификаторов пользователей.</param>
        /// <exception cref="AuthorizationRequiredException"/>
        private async Task<List<VKProfileChat>> GetUsers(List<ulong> usersIDs)
        {
            var request = new GetUsersRequest { UserIDs = usersIDs };

            for (int i = 0; i < MAX_ATTEMPTS_NUMBER; i++)
            {
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.AccessDenied || response.Error.ErrorType == VKErrors.AuthorizationFailed)
                    throw new AuthorizationRequiredException("Запрос провалился из-за необходимости выполнить авторизацию.");
                if (response.Error.ErrorType != VKErrors.None)
                {
                    if (i + 1 < MAX_ATTEMPTS_NUMBER)
                        await Task.Delay(1000);
                    continue;
                }

                var result = new List<VKProfileChat>();
                foreach (var user in response.Response)
                {
                    result.Add(new VKProfileChat
                    {
                        ID = user.ID,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Online = user.Online,
                        Photo50 = user.Photo50,
                        Sex = user.Sex
                    });
                }

                return result;
            }

            return null;
        }
    }
}
