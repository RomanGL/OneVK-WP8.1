using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет тег Last.fm.
    /// </summary>
    public class LFTag
    {
        /// <summary>
        /// Название тега.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на информацию о теге.
        /// </summary>
        [JsonProperty("url")]
        public string InfoURL { get; set; }
    }
}
