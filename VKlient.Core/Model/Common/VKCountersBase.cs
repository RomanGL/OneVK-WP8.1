using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Базовый класс счетчиков.
    /// </summary>
    public abstract class VKCountersBase
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
