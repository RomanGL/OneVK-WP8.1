using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет ответ на запрос получения информации о треке.
    /// </summary>
    public class LFTrackInfoResponse : LFResponse
    {
        /// <summary>
        /// Информация о треке.
        /// </summary>
        [JsonProperty("track")]
        public LFTrackInfo Track { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public override bool IsValid()
        {
            return Track != null;
        }
    }
}
