using Newtonsoft.Json;
using OneVK.Core.Messages;
using OneVK.Model.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис кэширования диалогов.
    /// </summary>
    internal sealed class DialogsCacheService : IDialogsCacheService
    {
        private const string DIALOGS_CACHE_FILE_NAME = "Dialogs.ovk";

        /// <summary>
        /// Кэширует коллекцию диалогов.
        /// </summary>
        /// <param name="dialogs">Коллекция диалогов для кэширования.</param>
        /// <exception cref="CacheDialogsException"/>
        public async Task CacheDialogs(IEnumerable<VKDialog> dialogs)
        {
            try
            {
                var file = await CreateCacheFile();
                string json = JsonConvert.SerializeObject(dialogs);
                await FileIO.WriteTextAsync(file, json);
            }
            catch (Exception ex)
            {
                throw new CacheDialogsException("Не удалось кэшировать коллекцию диалогов.", ex);
            }
        }

        /// <summary>
        /// Возвращает коллекцию диалогов из кэша.
        /// </summary>
        /// <exception cref="NoDialogsInCacheException"/>
        /// <exception cref="CacheDialogsException"/>
        public async Task<IEnumerable<VKDialog>> GetDialogs()
        {
            try
            {
                var file = await GetCachedFile();
                string json = await FileIO.ReadTextAsync(file);
                var dialogs = JsonConvert.DeserializeObject<List<VKDialog>>(json);
                return dialogs;
            }
            catch (FileNotFoundException ex)
            {
                throw new NoDialogsInCacheException("В кэше отсутствуют диалоги.", ex);
            }
            catch (Exception ex)
            {
                throw new CacheDialogsException("Не удалось получить коллекцию диалогов из кэша.", ex);
            }
        }

        /// <summary>
        /// Очищает кэш диалогов.
        /// </summary>
        /// <exception cref="ClearCacheException"/>
        public async Task ClearCache()
        {
            try
            {
                var file = await GetCachedFile();
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            catch (FileNotFoundException) { }
            catch (Exception ex)
            {
                throw new ClearCacheException("Не удалось очистить кэш диалогов.", ex);
            }
        }

        #region Utils

        /// <summary>
        /// Возвращает кэшированный файл диалогов при наличии.
        /// </summary>
        private static async Task<StorageFile> GetCachedFile()
        {
            return await ApplicationData.Current.LocalFolder.GetFileAsync(DIALOGS_CACHE_FILE_NAME);
        }

        /// <summary>
        /// Возвращает созданный файл для кэширования диалогов, перезаписывая его при наличии.
        /// </summary>
        private static async Task<StorageFile> CreateCacheFile()
        {
            return await ApplicationData.Current.LocalFolder.CreateFileAsync(DIALOGS_CACHE_FILE_NAME, 
                CreationCollisionOption.ReplaceExisting);
        }

        #endregion
    }
}
