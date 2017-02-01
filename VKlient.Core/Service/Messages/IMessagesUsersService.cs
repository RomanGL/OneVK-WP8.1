using OneVK.Model.Message;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneVK.Core.Messages;
using OneVK.Core;
using OneVK.Model.Profile;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис для работы с пользователями сообщений.
    /// </summary>
    internal interface IMessagesUsersService
    {
        /// <summary>
        /// Устанавливает пользователей для коллекции сообщений.
        /// </summary>
        /// <param name="messages">Коллекция сообщений.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        Task SetUsers(IEnumerable<VKMessage> messages);

        /// <summary>
        /// Возвращает пользователей указанной беседы.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        Task<IEnumerable<VKProfileShort>> GetConversationUsers(long convID);
    }
}
