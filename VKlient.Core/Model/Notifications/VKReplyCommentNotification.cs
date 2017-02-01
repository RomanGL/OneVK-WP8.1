using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение, когда был добавлен новый ответ на комментарий пользователя,
    /// комментарий к фото или к видео.
    /// </summary>
    public class VKReplyCommentNotification : VKNotificationBase, 
        INotificationParent<VKNotificationComment>, 
        INotificationFeedback<VKNotificationCommentFeedback>,
        INotificationReply
    {      
        /// <summary>
        /// Комментарий, на который оставлен ответ.
        /// </summary>
        [JsonProperty("parent")]
        public VKNotificationComment Parent { get; set; }

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
