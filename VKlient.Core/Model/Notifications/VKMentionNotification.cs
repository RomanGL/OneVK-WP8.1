using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение об упоминании пользователя в посте на чужой стене.
    /// </summary>
    public class VKMentionNotification : VKNotificationBase,
        INotificationFeedback<VKNotificationPostFeedback>,
        INotificationReply
    {
        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        [JsonProperty("feedback")]
        public VKNotificationPostFeedback Feedback { get; set; }

        /// <summary>
        /// Ответ пользователя на оповещение.
        /// </summary>
        [JsonProperty("reply")]
        public VKNotificationReply Reply { get; set; }
    }
}
