using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет альбом аудиозаписей Last.fm.
    /// </summary>
    public class LFAlbumBase : LFMediaElement
    {
        /// <summary>
        /// Количество воспроизведений.
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Исполнитель трека.
        /// </summary>
        [JsonProperty("artist")]
        public LFArtistBase Artist { get; set; }
    }
}
