using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Group;
using OneVK.Model.Profile;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой списковый ответ ВКонтакте на запрос.
    /// </summary>
    public class VKVideoExtendedItemsObject<T> : VKCountedItemsObject<T>
    {
        /// <summary>
        /// Коллекция объектов профилей.
        /// </summary>
        [JsonProperty("profiles")]
        public List<VKProfileBase> Profiles { get; set; }

        /// <summary>
        /// Коллекция объектов групп.
        /// </summary>
        [JsonProperty("groups")]
        public List<VKGroupBase> Groups { get; set; }
    }
}
