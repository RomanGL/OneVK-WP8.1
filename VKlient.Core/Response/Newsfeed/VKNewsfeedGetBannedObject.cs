using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой ответ сервера ВКонтакте
    /// на вызов метода newsfeed.getBanned.
    /// </summary>
    public sealed class VKNewsfeedGetBannedObject
    {
        /// <summary>
        /// Сисок идентификаторов сообществ, которые 
        /// пользоваотась скрыл из ленты новостей.
        /// </summary>
        [JsonProperty("groups")]
        public List<long> Groups { get; set; }

        /// <summary>
        /// Список идентификаторов друзей, которых
        /// пользовательскрыл из ленты новостей.
        /// </summary>
        [JsonProperty("members")]
        public List<long> Members { get; set; }
    }
}
