using Newtonsoft.Json;
using OneVK.Core.Json;
using System;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет ответ пользователя на оповещение.
    /// </summary>
    public class VKNotificationReply
    {
        /// <summary>
        /// Идентификатор ответа.
        /// </summary>
        [JsonProperty("id")]
        public uint ID { get; set; }

        /// <summary>
        /// Дата, когда оставлен ответ.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Текст ответа.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
