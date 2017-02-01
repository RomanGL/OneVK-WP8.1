using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос подобных исполнителей Last.fm.
    /// </summary>
    public class LFSimilarArtistsResponse : LFResponse
    {
        /// <summary>
        /// Объект списка исполнителей.
        /// </summary>
        [JsonProperty("similarartists")]
        public LFSimilarArtistsObject Data { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public override bool IsValid()
        {
            return Data != null && Data.IsValid();
        }
    }
}
