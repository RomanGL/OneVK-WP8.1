using Newtonsoft.Json;
using OneVK.Model.Common;

namespace OneVK.Model.Audio
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
