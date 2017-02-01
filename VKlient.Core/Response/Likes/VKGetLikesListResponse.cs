using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на базовый запрос списка лайкнувших.
    /// </summary>
    public class VKGetLikesListResponse
    {
        /// <summary>
        /// Количество элементов.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        [JsonProperty("items")]
        public List<long> Items { get; set; }
    }
}
