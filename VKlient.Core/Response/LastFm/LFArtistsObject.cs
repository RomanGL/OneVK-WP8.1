using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Audio;
using OneVK.Model.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет список исполнителей Last.fm.
    /// </summary>
    public class LFArtistsObject : ISupportValidation
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
