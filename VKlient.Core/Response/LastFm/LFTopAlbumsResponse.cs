using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос топовых треков Last.fm.
    /// </summary>
    public class LFTopAlbumsResponse : LFResponse
    {
        /// <summary>
        /// Объект списка треков.
        /// </summary>
        [JsonProperty("topalbums")]
        public LFAlbumsObject Data { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public override bool IsValid()
        {
            return Data != null && Data.IsValid();
        }
    }
}
