using OneVK.Model.Message;
using OneVK.Core.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис кэширования диалогов.
    /// </summary>
    internal interface IDialogsCacheService
    {
        /// <summary>
        /// Кэширует коллекцию диалогов.
        /// </summary>
        /// <param name="dialogs">Коллекция диалогов для кэширования.</param>
        /// <exception cref="CacheDialogsException"/>
        Task CacheDialogs(IEnumerable<VKDialog> dialogs);

        /// <summary>
        /// Возвращает коллекцию диалогов из кэша.
        /// </summary>
        /// <exception cref="NoDialogsInCacheException"/>
        /// <exception cref="CacheDialogsException"/>
        Task<IEnumerable<VKDialog>> GetDialogs();

        /// <summary>
        /// Очищает кэш диалогов.
        /// </summary>
        /// <exception cref="ClearCacheException"/>
        Task ClearCache();
    }
}
