using Newtonsoft.Json;
using System;
using OneVK.Core.Json;
using OneVK.Enums.Notifications;
using OneVK.Model.Photo;
using OneVK.Model.Video;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет комментарий оповещения.
    /// </summary>
    public class VKNotificationComment : IActionObjectContainer
    {
        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }

        /// <summary>
        /// Идентификатор автора комментария.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор пользователя, для которого оставлен ответ.
        /// </summary>
        [JsonProperty("reply_to_user")]
        public ulong ReplyToUserID { get; set; }

        /// <summary>
        /// Идентификатор комментария, на который оставлен ответ.
        /// </summary>
        [JsonProperty("reply_to_comment")]
        public uint ReplyToCommentID { get; set; }

        /// <summary>
        /// Время публикации комментария.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Текст комментария.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Пост, к которому был оставен комментарий.
        /// </summary>
        [JsonProperty("post")]
        public VKNotificationPost Post { get; set; }

        /// <summary>
        /// Фотография, к которой оставлен комментарий (только для типов
        /// <see cref="VKNotificationType.reply_comment_photo"/> и
        /// <see cref="VKNotificationType.like_comment_photo"/>).
        /// </summary>
        [JsonProperty("photo")]
        public VKPhoto Photo { get; set; }

        /// <summary>
        /// Видеозапись, к которой оставлен комментарий (только для типов
        /// <see cref="VKNotificationType.reply_comment_video"/> и
        /// <see cref="VKNotificationType.like_comment_video"/>).
        /// </summary>
        [JsonProperty("video")]
        public VKVideoBase Video { get; set; }

        /// <summary>
        /// Идентификатор автора комментария.
        /// </summary>
        [JsonIgnore]
        public long FromID { get { return OwnerID; } }

        /// <summary>
        /// Объект-инициатор оповещения.
        /// </summary>
        [JsonProperty("action_object")]
        [JsonConverter(typeof(NotificationActionObjectConverter))]
        public INotificationActionObject ActionObject { get; set; }
    }
}
