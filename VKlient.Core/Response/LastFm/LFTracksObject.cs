using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Audio;
using OneVK.Model.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет список треков Last.fm.
    /// </summary>
    public class LFTracksObject : ISupportValidation
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
