using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет расширенный трек Last.fm.
    /// </summary>
    public class LFAudioExtended : LFAudioBase
    {
        /// <summary>
        /// Идентификатор трека.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }        

        /// <summary>
        /// Теги композиции.
        /// </summary>
        [JsonProperty("toptags")]
        public LFTags Tags { get; set; }
    }
}
