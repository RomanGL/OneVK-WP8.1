using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение о новом комментарии под фотографией с упоминанием пользователя.
    /// </summary>
    public class VKMentionCommentPhotoNotification : VKNotificationBase,
        INotificationParent<VKNotificationPhoto>,
        INotificationFeedback<VKNotificationCommentFeedback>,
        INotificationReply
    {
        /// <summary>
        /// Фотография под которой оставлен комментарий с упоминанием.
        /// </summary>
        [JsonProperty("parent")]
        public VKNotificationPhoto Parent { get; set; }

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
