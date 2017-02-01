using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос получения списка
    /// общих с пользователем друзей.
    /// </summary>
    public class VKCommonFriendsObject
    {
        /// <summary>
        /// Идентификатор пользователя, общие друзья с которым
        /// получены
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Список идентификаторов общих друзей.
        /// </summary>
        [JsonProperty("common_friends")]
        public List<long> CommonFriends { get; set; }

        /// <summary>
        /// Общее количество общих друзей.
        /// </summary>
        [JsonProperty("common_count")]
        public int CommonCount { get; set; }
    }
}
