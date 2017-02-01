using System.Collections.Generic;
using System.Threading.Tasks;
using OneVK.Core.Models;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис кэширования изображений.
    /// </summary>
    public interface IImagesCacheService
    {
        /// <summary>
        /// Кэшировать изображение альбома трека.
        /// </summary>
        /// <param name="name">Имя исполнителя.</param>
        /// <param name="url">Ссылка на изображение.</param>
        Task<string> CacheAlbumImage(string name, string url);

        /// <summary>
        /// Получить ссылку на кэшированное изображение альбома трека.
        /// </summary>
        /// <param name="name">Название трека.</param>
        Task<string> GetAlbumImage(string name);

        /// <summary>
        /// Получить кэшированные изображения альбомов треков.
        /// </summary>
        /// <param name="count">Количество результатов.</param>
        Task<List<string>> GetCachedAlbumsImages(uint count);

        /// <summary>
        /// Кэшировать изображение исполнителя.
        /// </summary>
        /// <param name="name">Имя исполнителя.</param>
        /// <param name="url">Ссылка на изображение.</param>
        Task<string> CacheArtistImage(string name, string url);

        /// <summary>
        /// Получить ссылку на кэшированное изображение исполнителя.
        /// </summary>
        /// <param name="name">Имя исполнителя.</param>
        Task<string> GetArtistImage(string name);

        /// <summary>
        /// Получить кэшированные изображения исполнителей.
        /// </summary>
        /// <param name="count">Количество результатов.</param>
        Task<List<string>> GetCachedArtistImages(uint count);

        /// <summary>
        /// Очистить кэш изображений альбомов треков.
        /// </summary>
        Task<bool> ClearAlbumsCache();

        /// <summary>
        /// Очистить кэш изображений исполнителей.
        /// </summary>
        Task<bool> ClearArtistsCache();

        /// <summary>
        /// Возвращает размер кэша изображений альбомов треков.
        /// </summary>
        Task<FileSize> GetAlbumsCacheSize();

        /// <summary>
        /// Возвращает размер кэша изображений исполнителей.
        /// </summary>
        Task<FileSize> GetArtistsCacheSize();
    }
}
