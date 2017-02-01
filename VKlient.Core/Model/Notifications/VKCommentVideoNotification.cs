using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение о новом комментарии к фотографии пользователя.
    /// </summary>
    public class VKCommentVideoNotification : VKNotificationBase,
        INotificationParent<VKNotificationVideo>,
        INotificationFeedback<VKNotificationCommentFeedback>,
        INotificationReply
    {
        /// <summary>
        /// Видеозапись, к которой оставлен коммнтарий.
        /// </summary>
        [JsonProperty("parent")]
        public VKNotificationVideo Parent { get; set; }

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
