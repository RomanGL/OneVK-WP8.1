using OneVK.Core.Messages;
using System.Threading.Tasks;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис кэширования сообщений.
    /// </summary>
    internal interface IMessagesCacheService
    {
        /// <summary>
        /// Кэширует указанную беседу.
        /// </summary>
        /// <param name="conv">Беседа, которую требуется закэшировать.</param>
        /// <exception cref="CacheConversationException"/>
        Task CacheConversation(IConversation conv);

        /// <summary>
        /// Возвращает беседу из кэша.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        /// <exception cref="ConversationNotFoundInCacheException"/>
        /// <exception cref="CacheConversationException"/>
        Task<IConversation> GetConversation(long convID);
        
        /// <summary>
        /// Очищает кэш сообщений.
        /// </summary>
        /// <exception cref="ClearCacheException"/>
        Task ClearCache();
    }
}
