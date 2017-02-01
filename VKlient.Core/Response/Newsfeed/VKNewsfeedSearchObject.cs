using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Model.Wall;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой ответ сервера ВКонтакте
    /// на вызов метода newsfeed.search.
    /// </summary>
    public class VKNewsfeedSearchObject : VKCountedItemsObject<VKWallPost>
    {
        /// <summary>
        /// Общее количество результатов поиска.
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Смешение относительно начала списка.
        /// </summary>
        [JsonProperty("next_from")]
        public string NextFrom { get; set; }
    }
}
