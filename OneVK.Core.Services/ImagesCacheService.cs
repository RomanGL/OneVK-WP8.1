using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneVK.Core.Models;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.Web.Http;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис кэширование изображений.
    /// </summary>
    public sealed class ImagesCacheService : IImagesCacheService
    {
        private const string ALBUMS_FOLDER_NAME = "AlbumsCache";
        private const string ARTISTS_FOLDER_NAME = "ArtistsCache";

        /// <summary>
        /// Кэшировать изображение альбома трека.
        /// </summary>
        /// <param name="name">Название альбома.</param>
        /// <param name="url">Ссылка на изображение.</param>
        public async Task<string> CacheAlbumImage(string name, string url)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ALBUMS_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
                return await CacheAndGet(name.ToLower().Trim() + ".jpg", url, folder);
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Получить ссылку на кэшированное изображение альбома трека.
        /// </summary>
        /// <param name="name">Название трека.</param>
        public async Task<string> GetAlbumImage(string name)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ALBUMS_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
                return await Get(name.ToLower().Trim() + ".jpg", folder);
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Получить кэшированные изображения альбомов треков.
        /// </summary>
        /// <param name="count">Количество результатов.</param>
        public async Task<List<string>> GetCachedAlbumsImages(uint count)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ALBUMS_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
                var files = await folder.GetFilesAsync(CommonFileQuery.DefaultQuery, 0, count);
                return files.Select(f => f.Path).ToList();
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Кэшировать изображение исполнителя.
        /// </summary>
        /// <param name="name">Имя исполнителя.</param>
        /// <param name="url">Ссылка на изображение.</param>
        public async Task<string> CacheArtistImage(string name, string url)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ARTISTS_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
                return await CacheAndGet(name.ToLower().Trim() + ".jpg", url, folder);
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Получить ссылку на кэшированное изображение исполнителя.
        /// </summary>
        /// <param name="name">Имя исполнителя.</param>
        public async Task<string> GetArtistImage(string name)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ARTISTS_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
                return await Get(name.ToLower().Trim() + ".jpg", folder);
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Получить кэшированные изображения исполнителей.
        /// </summary>
        /// <param name="count">Количество результатов.</param>
        public async Task<List<string>> GetCachedArtistImages(uint count)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ARTISTS_FOLDER_NAME, CreationCollisionOption.OpenIfExists);
                var files = await folder.GetFilesAsync(CommonFileQuery.DefaultQuery, 0, count);
                return files.Select(f => f.Path).ToList();
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Очистить кэш изображений альбомов треков.
        /// </summary>
        public async Task<bool> ClearAlbumsCache()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(ALBUMS_FOLDER_NAME);
                await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Очистить кэш изображений исполнителей.
        /// </summary>
        public async Task<bool> ClearArtistsCache()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(ARTISTS_FOLDER_NAME);
                await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Возвращает размер кэша изображений альбомов треков.
        /// </summary>
        public async Task<FileSize> GetAlbumsCacheSize()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(ALBUMS_FOLDER_NAME );
                var basic = await folder.GetBasicPropertiesAsync();
                IDictionary<string, object> properties = await folder.Properties.RetrievePropertiesAsync(new[] { "System.Size" });

                return FileSize.FromBytes((ulong)properties["System.Size"]);
            }
            catch (Exception) { return FileSize.FromBytes(0); }
        }

        /// <summary>
        /// Возвращает размер кэша изображений исполнителей.
        /// </summary>
        public async Task<FileSize> GetArtistsCacheSize()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(ARTISTS_FOLDER_NAME);
                var properties = await folder.GetBasicPropertiesAsync();
                return FileSize.FromBytes(properties.Size);
            }
            catch (Exception) { return FileSize.FromBytes(0); }
        }

        /// <summary>
        /// Возвращает папку из хранилища с указанным именем.
        /// </summary>
        /// <param name="folderName">Имя папки.</param>
        private async Task<StorageFolder> GetCreateFolder(string folderName)
        {
            return await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Кэширует и возвращает путь к загруженному файлу.
        /// </summary>
        /// <param name="name">Имя файла.</param>
        /// <param name="url">Ссылка на файл.</param>
        /// <param name="folder">Папка, в которую требуется загрузить.</param>
        private async Task<string> CacheAndGet(string name, string url, StorageFolder folder)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(url));
                var file = await folder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
                if ((await file.GetBasicPropertiesAsync()).Size <= 1024)
                    await FileIO.WriteBufferAsync(file, await response.Content.ReadAsBufferAsync());

                if ((await file.GetBasicPropertiesAsync()).Size <= 1024)
                    return null;

                return file.Path;
            }
        }

        /// <summary>
        /// Возвращает путь к файлу.
        /// </summary>
        /// <param name="name">Имя файла.</param>
        /// <param name="folder">Папка.</param>
        private async Task<string> Get(string name, StorageFolder folder)
        {
            var file = await folder.GetFileAsync(name);
            if ((await file.GetBasicPropertiesAsync()).Size <= 1024) return null;
            return file.Path;
        }
    }
}
