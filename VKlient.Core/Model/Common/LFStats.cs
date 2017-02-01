using Newtonsoft.Json;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет статистику элемента Last.fm.
    /// </summary>
    public class LFStats
    {
        /// <summary>
        /// Количество слушателей.
        /// </summary>
        [JsonProperty("listeners")]
        public int ListenersCount { get; set; }

        /// <summary>
        /// Количество воспроизведений.
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }
    }
}
