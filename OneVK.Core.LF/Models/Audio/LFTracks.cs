using Newtonsoft.Json;
using System.Collections.Generic;
namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет список треков Last.fm.
    /// </summary>
    public class LFTracks : ISupportValidation
    {
        /// <summary>
        /// Список треков.
        /// </summary>
        [JsonProperty("track")]
        public List<LFAudioBase> Tracks { get; set; }

        /// <summary>
        /// Дополнительная информация об ответе.
        /// </summary>
        [JsonProperty("@attr")]
        public LFAditionalData AditionalData { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public bool IsValid()
        {
            return Tracks != null && AditionalData != null;
        }
    }
}
