using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Audio;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет список подобных исполнителей Last.fm.
    /// </summary>
    public class LFSimilarArtistsObject : ISupportValidation
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
