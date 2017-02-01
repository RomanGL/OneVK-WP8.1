using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Model.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет инфрмацию об оповещении-комментарии.
    /// </summary>
    public class VKNotificationCommentFeedback : VKNotificationFeedback
    {
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
        /// Дата, когда был оставлен ответ.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Вложения к комментарию.
        /// </summary>
        [JsonProperty("attachments")]
        public List<VKAttachment> Attachments { get; set; }
    }
}
