using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Group;
using OneVK.Model.Newsfeed;
using OneVK.Model.Profile;

namespace OneVK.Response
{
    /// <summary>
    /// Ответ сервера ВКонтакте на
    /// вызов метода newsfeed.get.
    /// </summary>
    public class VKNewsfeedGetObject : VKBaseItemsObject<VKNewsfeedPost>
    {
        /// <summary>
        /// Информация о пользователях, которые
        /// находятся в списке новостей.
        /// </summary>
        [JsonProperty("profiles")]
        public List<VKProfileBase> Profiles { get; set; }

        /// <summary>
        /// Информация о группах, которые
        /// находятся в списке новостей.
        /// </summary>
        [JsonProperty("groups")]
        public List<VKGroupBase> Groups { get; set; }

        /// <summary>
        /// From, который необходимо передать, для того, 
        /// чтобы получить следующую часть новостей.
        /// </summary>
        [JsonProperty("next_from")]
        public string NextFrom { get; set; }
    }
}
