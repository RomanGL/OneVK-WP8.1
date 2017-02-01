using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет расширенного исполнителя Last.fm.
    /// </summary>
    public class LFArtistExtended : LFArtistBase
    {
        /// <summary>
        /// Находится ли исполнитель в турне.
        /// </summary>
        [JsonProperty("ontour")]
        public LFArtistOnTour OnTour { get; set; }

        /// <summary>
        /// Возможна ли потоковая передача с Last.fm.
        /// </summary>
        [JsonProperty("streamable")]
        public LFStreamable Streamable { get; set; }

        /// <summary>
        /// Статистика исполнителя.
        /// </summary>
        [JsonProperty("stats")]
        public LFStats Stats { get; set; }

        /// <summary>
        /// Подобные исполнители.
        /// </summary>
        [JsonProperty("similar")]
        public LFSimilarArtists Similar { get; set; }

        /// <summary>
        /// Теги исполнителя.
        /// </summary>
        [JsonProperty("tags")]
        public LFTags Tags { get; set; }

        /// <summary>
        /// Количество воспроизведений.
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Количество слушателей.
        /// </summary>
        [JsonProperty("listeners")]
        public int ListenersCount { get; set; }
    }
}
