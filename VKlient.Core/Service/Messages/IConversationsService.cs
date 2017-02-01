using OneVK.Core.Messages;
using OneVK.Core;
using System.Threading.Tasks;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис для работы с беседами.
    /// </summary>
    internal interface IConversationsService
    {
        /// <summary>
        /// Загружает чат по указанному идентификатору и возвращает его.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        /// <param name="reset">Сбросить значение кэша и загрузить повторно.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        Task<IChat> GetChat(uint chatID);

        /// <summary>
        /// Загружает диалог по указанному идентификатору пользователя и возвращает его.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="reset">Сбросить значение кэша и загрузить повторно.</param>
        /// <exception cref="CacheConversationException"/>
        /// <exception cref="AuthorizationRequiredException"/>
        Task<IDialog> GetDialog(ulong userID);
    }
}
