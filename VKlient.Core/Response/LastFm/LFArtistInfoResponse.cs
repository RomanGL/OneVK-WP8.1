using Newtonsoft.Json;
using OneVK.Model.Audio;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервиса на запрос информации об исполнителе.
    /// </summary>
    public class LFArtistInfoResponse : LFResponse
    {
        /// <summary>
        /// Информация об исполнителе.
        /// </summary>
        [JsonProperty("artist")]
        public LFArtistExtended Artist { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public override bool IsValid()
        {
            return Artist != null;
        }
    }
}
