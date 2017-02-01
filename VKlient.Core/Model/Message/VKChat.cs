using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OneVK.Model.Message
{
    /// <summary>
    /// Представляет собой беседу ВКонтакте.
    /// </summary>
    public class VKChat
    {
        /// <summary>
        /// Тип диалога.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Заголовок беседы.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Идентификатор создателя беседы.
        /// </summary>
        [JsonProperty("admin_id")]
        public long AdminID { get; set; }
        /// <summary>
        /// Коллекция идентификаторов участников беседы.
        /// </summary>
        [JsonProperty("users")]
        public List<long> Users { get; set; }
        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
    }
}
