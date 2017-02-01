using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using OneVK.Helpers;
using OneVK.Model.Message;
using OneVK.Core;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет сервис кэширования сообщений.
    /// </summary>
    internal sealed class MessagesCacheServiceOld
    {
        private const string MessagesFolderName = "Messages";
        private const string DialogsCacheFileName = "Dialogs";
        private const string ConversationFileName = "Conv";
        private const string CacheFileExtension = ".ovk";

        /// <summary>
        /// Кэширует список диалогов и возвращает успех операции.
        /// </summary>
        /// <param name="dialogs">Список диалогов.</param>
        public async Task<bool> CacheDialogs(List<VKDialog> dialogs)
        {
            var file = await CreateCacheFile(GetDialogsFileName());
            if (file == null) return false;

            string content = JsonSerializationHelper.SerializeToJson(dialogs);
            bool result = await FileHelper.WriteTextToFile(file, content);
            return result;
        }

        /// <summary>
        /// Возвращает список кэшированных диалогов.
        /// </summary>
        public async Task<List<VKDialog>> GetDialogsFromCache()
        {
            var file = await GetCachedFile(GetDialogsFileName());
            if (file == null) return null;

            string content = await FileHelper.ReadTextFromFile(file);
            if (String.IsNullOrEmpty(content)) return null;

            var result = JsonSerializationHelper.DeserializeFromJson<List<VKDialog>>(content);
            return result;
        }

        /// <summary>
        /// Кэширует список сообщений беседы и возвращает успех операции.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        /// <param name="messages">Список сообщений беседы.</param>
        public async Task<bool> CacheConversation(long convID, CachedConversationOld messages)
        {
            var file = await CreateCacheFile(GetConversationFileName(convID));
            if (file == null) return false;

            string content = JsonSerializationHelper.SerializeToJson(messages);
            bool result = await FileHelper.WriteTextToFile(file, content);
            return result;
        }

        /// <summary>
        /// Возвращает список кэшированных сообщений указанной беседы.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        public async Task<CachedConversationOld> GetConversationFromCache(long convID)
        {            
            var file = await GetCachedFile(GetConversationFileName(convID));
            if (file == null) return null;

            string content = await FileHelper.ReadTextFromFile(file);
            if (String.IsNullOrEmpty(content)) return null;

            var result = JsonSerializationHelper.DeserializeFromJson<CachedConversationOld>(content);
            return result;
        }

        /// <summary>
        /// Очистить кэш сообщений.
        /// </summary>
        public async Task<bool> ClearConversationsCache()
        {
            try
            {
                await (await GetMessagesFolder()).DeleteAsync(StorageDeleteOption.PermanentDelete);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Возвращает кэшированный файл при его наличии.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        private static async Task<StorageFile> GetCachedFile(string fileName)
        {
            StorageFolder messagesFolder = await GetMessagesFolder();
            if (messagesFolder == null) return null;

            try { return await messagesFolder.GetFileAsync(fileName); }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Возвращает созданный файл для кэширования, перезаписывая его при наличии.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        private static async Task<StorageFile> CreateCacheFile(string fileName)
        {
            StorageFolder messagesFolder = await GetMessagesFolder();
            if (messagesFolder == null) return null;

            try { return await messagesFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting); }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Возвращает папку, в которой кэшируются сообщения.
        /// </summary>
        private static async Task<StorageFolder> GetMessagesFolder()
        {
            try { return await ApplicationData.Current.LocalFolder.CreateFolderAsync(MessagesFolderName, CreationCollisionOption.OpenIfExists); }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Возвращает имя файла для указанной беседы.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        private static string GetConversationFileName(long convID)
        {
            return ConversationFileName + convID + CacheFileExtension;
        }

        /// <summary>
        /// Возвращает имя файла кэшированных диалогов.
        /// </summary>
        private static string GetDialogsFileName()
        {
            return DialogsCacheFileName + CacheFileExtension;
        }
    }
}
