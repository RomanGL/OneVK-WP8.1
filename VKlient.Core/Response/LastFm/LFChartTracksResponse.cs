using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервера Last.fm на запрос чартов композиций.
    /// </summary>
    public class LFChartTracksResponse : LFResponse
    {      
        /// <summary>
        /// Объект списка треков.
        /// </summary>
        [JsonProperty("tracks")]
        public LFTracksObject Items { get; set; }

        /// <summary>
        /// Являются ли данные валидными.
        /// </summary>
        public override bool IsValid()
        {
            return Items != null && Items.IsValid();
        }
    }
}
