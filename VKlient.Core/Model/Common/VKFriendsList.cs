using Newtonsoft.Json;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет список друзей ВКонтакте.
    /// </summary>
    public class VKFriendsList
    {
        /// <summary>
        /// Название списка.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор списка.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}
