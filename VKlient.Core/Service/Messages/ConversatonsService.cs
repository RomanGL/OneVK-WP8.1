using Microsoft.Practices.ServiceLocation;
using OneVK.Core;
using OneVK.Core.Collections;
using OneVK.Core.Messages;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Model.Profile;
using OneVK.Request;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис для работы с беседами.
    /// </summary>
    internal class ConversatonsService : IConversationsService
    {
        private const int MAX_ATTEMPTS_NUMBER = 5;

        private IMessagesCacheService cacheService;
        private readonly ConcurrentDictionary<ulong, Task<IDialog>> dialogsTasks =
            new ConcurrentDictionary<ulong, Task<IDialog>>();
        private readonly ConcurrentDictionary<uint, Task<IChat>> chatsTasks =
            new ConcurrentDictionary<uint, Task<IChat>>();

        /// <summary>
        /// Загружает чат по указанному идентификатору и возвращает его.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        public Task<IChat> GetChat(uint chatID)
        {
            return chatsTasks.GetOrAdd(chatID, async id =>
            {
                if (cacheService == null) cacheService = ServiceLocator.Current.GetInstance<IMessagesCacheService>();

                Task<IChat> completedTask = null;
                var request = new GetChatRequest
                {
                    ChatID = id,
                    Fields = new List<VKUserFields> { VKUserFields.photo_50, VKUserFields.sex, VKUserFields.online }
                };

                for (int i = 0; i < MAX_ATTEMPTS_NUMBER; i++)
                {
                    var response = await request.ExecuteAsync();

                    if (response.Error.ErrorType == VKErrors.AccessDenied || response.Error.ErrorType == VKErrors.AuthorizationFailed)
                    {
                        chatsTasks.TryRemove(id, out completedTask);
                        throw new AuthorizationRequiredException("Запрос провалился из-за необходимости выполнить авторизацию.");
                    }
                    if (response.Error.ErrorType != VKErrors.None)
                    {
                        if (i + 1 < MAX_ATTEMPTS_NUMBER)
                            await Task.Delay(1000);
                        continue;
                    }

                    IChat chat = new Chat();
                    chat.AdminID = response.Response.AdminID;
                    chat.ChatID = response.Response.ID;
                    chat.Title = response.Response.Title;
                    chat.Messages = new MessagesCollection(response.Response.ID);
                    chat.Users = new ObservableCollection<VKProfileChat>(response.Response.Users);

                    await cacheService.CacheConversation(chat);

                    chatsTasks.TryRemove(id, out completedTask);
                    return chat;
                }

                chatsTasks.TryRemove(id, out completedTask);
                throw new CacheConversationException("Не удалось загрузить информацию о чате.");
            });
        }

        /// <summary>
        /// Загружает диалог по указанному идентификатору пользователя и возвращает его.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        public Task<IDialog> GetDialog(ulong userID)
        {
            return dialogsTasks.GetOrAdd(userID, async id =>
            {
                if (cacheService == null) cacheService = ServiceLocator.Current.GetInstance<IMessagesCacheService>();

                Task<IDialog> completedTask = null;var request = new GetUsersRequest { UserID = id };
                for (int i = 0; i < 5; i++)
                {
                    var response = await request.ExecuteAsync();

                    if (response.Error.ErrorType == VKErrors.AccessDenied || response.Error.ErrorType == VKErrors.AuthorizationFailed)
                    {
                        dialogsTasks.TryRemove(id, out completedTask);
                        throw new AuthorizationRequiredException("Запрос провалился из-за необходимости выполнить авторизацию.");
                    }
                    if (response.Error.ErrorType != VKErrors.None)
                    {
                        if (i + 1 < 5)
                            await Task.Delay(1000);
                        continue;
                    }

                    var respUser = response.Response[0];

                    IDialog dialog = new Dialog();
                    dialog.UserID = respUser.ID;
                    dialog.User = new VKProfileShort
                    {
                        ID = respUser.ID,
                        FirstName = respUser.FirstName,
                        LastName = respUser.LastName,
                        Online = respUser.Online,
                        Photo50 = respUser.Photo50,
                        Sex = respUser.Sex
                    };
                    dialog.Title = respUser.FullName;
                    dialog.Messages = new MessagesCollection(respUser.ID);
                    await cacheService.CacheConversation(dialog);

                    dialogsTasks.TryRemove(id, out completedTask);
                    return dialog;
                }

                dialogsTasks.TryRemove(id, out completedTask);
                throw new CacheConversationException("Не удалось загрузить информацию о диалоге.");
            });
        }
    }
}
