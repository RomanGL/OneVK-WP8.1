using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет список исполнителей Last.fm.
    /// </summary>
    public class LFArtists : ISupportValidation
    {
        /// <summary>
        /// Список исполнителей.
        /// </summary>
        [JsonProperty("artist")]
        public List<LFArtistExtended> Artists { get; set; }

        /// <summary>
        /// Дополнительная информация об ответе.
        /// </summary>
        [JsonProperty("@attr")]
        public LFAditionalData AditionalData { get; set; }

        /// <summary>
        /// Валидны ли данные.
        /// </summary>
        public bool IsValid()
        {
            return Artists != null && AditionalData != null;
        }
    }
}
