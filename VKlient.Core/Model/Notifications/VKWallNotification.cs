using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение о новой записи на стене с упоминанием пользователя.
    /// </summary>
    public class VKWallNotification : VKNotificationBase,
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
