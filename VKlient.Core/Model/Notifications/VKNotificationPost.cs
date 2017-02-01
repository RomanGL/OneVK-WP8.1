using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Model.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет пост оповещения.
    /// </summary>
    public class VKNotificationPost : IActionObjectContainer
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }

        /// <summary>
        /// Идентификатор владельца записи.
        /// </summary>
        [JsonProperty("to_id")]
        public long ToID { get; set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего запись.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; }

        /// <summary>
        /// Время публикации поста.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Текст поста.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Вложения к посту.
        /// </summary>
        [JsonProperty("attachments")]
        public List<VKAttachment> Attachments { get; set; }
        
        /// <summary>
        /// Информация о местоположении.
        /// </summary>
        [JsonProperty("geo")]
        public VKGeo Geo { get; set; }

        /// <summary>
        /// Идентификатор владельца записи если она скопирована с чужой стены.
        /// </summary>
        [JsonProperty("copy_owner_id")]
        public long CopyOwnerID { get; set; }

        /// <summary>
        /// Идентификатор скопированной записи на стене ее владельца
        /// </summary>
        [JsonProperty("copy_post_id")]
        public ulong CopyPostID { get; set; }

        /// <summary>
        /// Коллекция репостов.
        /// </summary>
        [JsonProperty("copy_history")]
        public List<VKNotificationPost> CopyHistory { get; set; }

        /// <summary>
        /// Объект-инициатор оповещения.
        /// </summary>
        [JsonProperty("action_object")]
        [JsonConverter(typeof(NotificationActionObjectConverter))]
        public INotificationActionObject ActionObject { get; set; }
    }
}
