using Newtonsoft.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение о публикации предложенного поста.
    /// </summary>
    public class VKWallPublishNotification : VKNotificationBase,
        INotificationFeedback<VKNotificationPostFeedback>
    {
        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        [JsonProperty("feedback")]
        public VKNotificationPostFeedback Feedback { get; set; }
    }
}
