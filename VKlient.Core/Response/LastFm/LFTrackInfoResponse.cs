using Newtonsoft.Json;
using OneVK.Model.Audio;

namespace OneVK.Response
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
