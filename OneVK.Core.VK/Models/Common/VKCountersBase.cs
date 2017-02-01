using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Базовый класс счетчиков.
    /// </summary>
    [ImplementPropertyChanged]
    public abstract class VKCountersBase : BindableBase
    {
        /// <summary>
        /// Количество фотографий.
        /// </summary>
        [JsonProperty("photos")]
        public uint PhotosCount { get; set; }
        /// <summary>
        /// Количество альбомов фотографий.
        /// </summary>
        [JsonProperty("albums")]
        public uint AlbumsCount { get; set; }
        /// <summary>
        /// Количество видео.
        /// </summary>
        [JsonProperty("videos")]
        public uint VideosCount { get; set; }
        /// <summary>
        /// Количество аудиозаписей.
        /// </summary>
        [JsonProperty("audios")]
        public uint AudiosCount { get; set; }
    }
}
