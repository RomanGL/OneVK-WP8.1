using System.Collections.Generic;
using System.Threading.Tasks;
using OneVK.Core.Models;
using Windows.Storage.Streams;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис кэширования аудиозаписей.
    /// </summary>
    public interface IAudioCacheService
    {                
        /// <summary>
        /// Возвращает список кэшированных треков.
        /// </summary>
        /// <param name="offset">Смещение относительно начала списка.</param>
        /// <param name="count">Количество результатов.</param>
        Task<List<IVKAudioTrack>> GetCachedTracks(uint offset, uint count);

        /// <summary>
        /// Очистить кэш аудиозаписей.
        /// </summary>
        Task<bool> ClearCache();

        /// <summary>
        /// Возвращает размер кэша.
        /// </summary>
        Task<FileSize> GetCacheSize();
    }
}