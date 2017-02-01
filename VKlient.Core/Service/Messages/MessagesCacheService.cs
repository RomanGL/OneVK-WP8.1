using Newtonsoft.Json;
using OneVK.Core.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace OneVK.Service.Messages
{
    /// <summary>
    /// Представляет сервис кэширования сообщений.
    /// </summary>
    internal sealed class MessagesCacheService : IMessagesCacheService
    {
        private const string MESSAGES_FOLDER_NAME = "Messages";
        private const string CONVERSATION_FILE_NAME = "Conv";
        private const string CACHE_FILE_EXTENSION = ".ovk";

        private readonly object lockObject = new object();
        private readonly Dictionary<long, IConversation> runtimeCache = new Dictionary<long, IConversation>();

        /// <summary>
        /// Кэширует указанную беседу.
        /// </summary>
        /// <param name="conv">Беседа, которую требуется закэшировать.</param>
        /// <exception cref="CacheConversationException"/>
        public Task CacheConversation(IConversation conv)
        {
            try
            {
                return Task.Run(() =>
                {
                    string json = JsonConvert.SerializeObject(conv);

                    lock (lockObject)
                    {
                        var fileTask = CreateCacheFile(GetConversationFileName(conv.ID));
                        fileTask.Wait();
                        FileIO.WriteTextAsync(fileTask.Result, json).AsTask().Wait();

                        runtimeCache[conv.ID] = conv;
                    }
                });
            }
            catch (Exception ex)
            {
                throw new CacheConversationException("Не удалось кэшировать беседу.", ex);
            }            
        }

        /// <summary>
        /// Возвращает беседу из кэша.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        /// <exception cref="ConversationNotFoundInCacheException"/>
        /// <exception cref="CacheConversationException"/>
        public Task<IConversation> GetConversation(long convID)
        {
            try
            {
                return Task.Run(() =>
                {
                    lock (lockObject)
                    {
                        if (runtimeCache.ContainsKey(convID))
                            return runtimeCache[convID];

                        var fileTask = GetCachedFile(GetConversationFileName(convID));
                        fileTask.Wait();

                        var readTask = FileIO.ReadTextAsync(fileTask.Result);
                        readTask.AsTask().Wait();
                        string json = readTask.GetResults();

                        var conversation = JsonConvert.DeserializeObject<IConversation>(json);
                        runtimeCache[convID] = conversation;

                        return conversation;
                    }
                });
            }
            catch (FileNotFoundException ex)
            {
                throw new ConversationNotFoundInCacheException("Беседа с указанным идентификатором отсутствует в кэше.", ex);
            }
            catch (Exception ex)
            {
                throw new CacheConversationException("Не удалось получить беседу из кэша.", ex);
            }            
        }

        /// <summary>
        /// Очищает кэш сообщений.
        /// </summary>
        /// <exception cref="ClearCacheException"/>
        public Task ClearCache()
        {
            try
            {
                return Task.Run(() =>
                {
                    lock (lockObject)
                    {
                        runtimeCache.Clear();

                        var folderTask = GetMessagesFolder();
                        folderTask.Wait();

                        folderTask.Result.DeleteAsync(StorageDeleteOption.PermanentDelete).AsTask().Wait();
                    }
                });
            }
            catch (Exception ex)
            {
                throw new ClearCacheException("Не удалось очистить кэш сообщений.", ex);
            }            
        }

        #region Utils

        /// <summary>
        /// Возвращает папку, в которой кэшируются сообщения.
        /// </summary>
        private static async Task<StorageFolder> GetMessagesFolder()
        {
            return await ApplicationData.Current.LocalFolder.CreateFolderAsync(
                MESSAGES_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Возвращает кэшированный файл в папке сообщений при его наличии.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        private static async Task<StorageFile> GetCachedFile(string fileName)
        {
            StorageFolder messagesFolder = await GetMessagesFolder();
            return await messagesFolder.GetFileAsync(fileName);
        }

        /// <summary>
        /// Возвращает созданный файл для кэширования в папке сообщений, перезаписывая его при наличии.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        private static async Task<StorageFile> CreateCacheFile(string fileName)
        {
            StorageFolder messagesFolder = await GetMessagesFolder();
            return await messagesFolder.CreateFileAsync(
                fileName, CreationCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// Возвращает имя файла для указанной беседы.
        /// </summary>
        /// <param name="convID">Идентификатор беседы.</param>
        private static string GetConversationFileName(long convID)
        {
            return CONVERSATION_FILE_NAME + convID + CACHE_FILE_EXTENSION;
        }

        public Task<IConversation> CacheAndGetConversation(long convID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
