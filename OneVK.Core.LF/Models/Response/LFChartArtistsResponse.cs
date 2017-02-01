using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет ответ сервера Last.fm на запрос чартов исполнителей.
    /// </summary>
    public class LFChartArtistsResponse : LFResponse
    {
        /// <summary>
        /// Объект списка исполнителей.
        /// </summary>
        [JsonProperty("artists")]
        public LFArtists Data { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public override bool IsValid()
        {
            return Data != null && Data.IsValid();
        }
    }
}
