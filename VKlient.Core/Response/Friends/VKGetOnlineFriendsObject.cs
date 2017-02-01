using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Response
{
    /// <summary>
    /// Представлет собой ответ сервера ВКонтакте на запрос 
    /// получения списка друзей, находящихся в сети.
    /// </summary>
    public class VKGetOnlineFriendsObject
    {
        [JsonProperty("online")]
        public List<long> Online { get; set; }

        [JsonProperty("online_mobile")]
        public List<long> OnlineMobile { get; set; }
    }
}
