using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OneVK.Core.Models;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис кэширования аудиозаписей.
    /// </summary>
    public sealed class AudioCacheService : IAudioCacheService
    {
        private const string AUDIOS_FOLDER_NAME = "AudiosCache";

        /// <summary>
        /// Возвращает список кэшированных треков.
        /// </summary>
        /// <param name="offset">Смещение относительно начала списка.</param>
        /// <param name="count">Количество результатов.</param>
        public async Task<List<IVKAudioTrack>> GetCachedTracks(uint offset, uint count)
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(AUDIOS_FOLDER_NAME);
                var files = await folder.GetFilesAsync(CommonFileQuery.DefaultQuery, offset, count);
                if (files.Count == 0) return null;
                var result = new List<IVKAudioTrack>();

                foreach (var file in files)
                {
                    var musicProperties = await file.Properties.GetMusicPropertiesAsync();
                    result.Add(new AudioTrack
                    {
                        Title = musicProperties.Title,
                        Artist = musicProperties.Artist
                    });
                }

                return result;
            }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Очистить кэш аудиозаписей.
        /// </summary>
        public async Task<bool> ClearCache()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(AUDIOS_FOLDER_NAME);
                await folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>
        /// Возвращает размер кэша.
        /// </summary>
        public async Task<FileSize> GetCacheSize()
        {
            try
            {
                var folder = await ApplicationData.Current.LocalFolder.GetFolderAsync(AUDIOS_FOLDER_NAME);
                var properties = await folder.GetBasicPropertiesAsync();
                return FileSize.FromBytes(properties.Size);
            }
            catch (Exception) { return FileSize.FromBytes(0); }
        }
    }
}
