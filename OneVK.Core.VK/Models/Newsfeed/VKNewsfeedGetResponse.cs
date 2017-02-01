using Newtonsoft.Json;
using OneVK.Core.VK.Models.Common;
using OneVK.Core.VK.Models.Groups;
using OneVK.Core.VK.Models.Users;
using System.Collections.Generic;

namespace OneVK.Core.VK.Models.Newsfeed
{
    /// <summary>
    /// Ответ сервера ВКонтакте на вызов метода newsfeed.get.
    /// </summary>
    public sealed class VKNewsfeedGetResponse : VKBaseItemsObject<VKNewsfeedItem>
    {
        /// <summary>
        /// Информация о пользователях, которые
        /// находятся в списке новостей.
        /// </summary>
        [JsonProperty("profiles")]
        public List<VKUser> Profiles { get; set; }

        /// <summary>
        /// Информация о группах, которые находятся в списке новостей.
        /// </summary>
        [JsonProperty("groups")]
        public List<VKGroup> Groups { get; set; }

        /// <summary>
        /// From, который необходимо передать, для того, 
        /// чтобы получить следующую часть новостей.
        /// </summary>
        [JsonProperty("next_from")]
        public string NextFrom { get; set; }
    }
}
