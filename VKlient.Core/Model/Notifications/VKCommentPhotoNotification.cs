using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение о новом комментарии к фотографии пользователя.
    /// </summary>
    public class VKCommentPhotoNotification : VKNotificationBase,
        INotificationParent<VKNotificationPhoto>,
        INotificationFeedback<VKNotificationCommentFeedback>,
        INotificationReply
    {
        /// <summary>
        /// Фотография, к которой оставлен коммнтарий.
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
