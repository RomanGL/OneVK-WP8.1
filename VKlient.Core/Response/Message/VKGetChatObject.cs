using System.Collections.Generic;
using Newtonsoft.Json;
using OneVK.Model.Profile;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос информации о чате.
    /// </summary>
    public class VKGetChatObject
    {
        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        [JsonProperty("id")]
        public uint ID { get; set; }
        /// <summary>
        /// Идентификатор создателя беседы.
        /// </summary>
        [JsonProperty("admin_id")]
        public ulong AdminID { get; set; }
        /// <summary>
        /// Список пользователей в беседе.
        /// </summary>
        [JsonProperty("users")]
        public List<VKProfileChat> Users { get; set; }
        /// <summary>
        /// Заголовок беседы.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

    }
}
