using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет список подобных исполнителей Last.fm.
    /// </summary>
    public class LFSimilarArtists : ISupportValidation
    {
        /// <summary>
        /// Список подобных исполнителей.
        /// </summary>
        [JsonProperty("artist")]
        public List<LFSimilarArtist> Artists { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public bool IsValid()
        {
            return Artists != null;
        }
    }
}
