using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет объект тегов Last.fm.
    /// </summary>
    public class LFTags
    {
        /// <summary>
        /// Список тегов.
        /// </summary>
        [JsonProperty("tag")]
        public List<LFTag> Tags { get; set; }
    }
}
