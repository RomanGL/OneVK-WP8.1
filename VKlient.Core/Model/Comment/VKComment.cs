using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OneVK.Model.Common;
using OneVK.Core.Json;

namespace OneVK.Model.Comment
{
    /// <summary>
    /// Комментарий к записи на стене ВКонтакте.
    /// </summary>
    public class VKComment
    {
        /// <summary>
        /// Идентификатор автора комментария.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; }
        /// <summary>
        /// Дата создания комментария.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }
        /// <summary>
        /// Текст комментария.
        /// </summary>
        [JsonProperty("text")]
        public string Comment { get; set; }
        /// <summary>
        /// Идентификатор пользователя или сообщества, в
        /// ответ которому оставлен текущий комментарий
        /// (если применимо).
        /// </summary>
        [JsonProperty("reply_to_user")]
        public long ReplyToUserID { get; set; }
        /// <summary>
        /// Идентификатор комментария, в ответ на который
        /// оставлен текущий (если применимо).
        /// </summary>
        [JsonProperty("reply_to_comment")]
        public ulong ReplyToCommentID { get; set; }
        /// <summary>
        /// Коллекция вложений.
        /// </summary>
        [JsonProperty("attachments")]
        public List<VKCommentAttachment> Attachments { get; set; }
        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }
        /// <summary>
        /// Информация об отметках "Мне нравится".
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes { get; set; }
    }
}
