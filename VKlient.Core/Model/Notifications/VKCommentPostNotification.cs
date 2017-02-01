using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение, когда был добавлен новый ответ на комментарий пользователя.
    /// </summary>
    public class VKCommentPostNotification : VKNotificationBase,
        INotificationParent<VKNotificationPost>,
        INotificationFeedback<VKNotificationCommentFeedback>,
        INotificationReply
    {
        /// <summary>
        /// Пост, к которому оставлен комментарий.
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
