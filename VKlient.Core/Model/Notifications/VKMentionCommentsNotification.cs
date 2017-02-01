using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение о новом комментарии с упоминанием пользователя.
    /// </summary>
    public class VKMentionCommentsNotification : VKNotificationBase,
        INotificationParent<VKNotificationPost>,
        INotificationFeedback<VKNotificationCommentFeedback>,
        INotificationReply
    {
        /// <summary>
        /// Пост, к которому оставлен комментарий с упоминанием.
        /// </summary>
        [JsonProperty("parent")]
        public VKNotificationPost Parent { get; set; }

        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        [JsonProperty("feedback")]
        public VKNotificationCommentFeedback Feedback { get; set; }

        /// <summary>
        /// Ответ пользователя на оповещение.
        /// </summary>
        [JsonProperty("reply")]
        public VKNotificationReply Reply { get; set; }
    }
}
