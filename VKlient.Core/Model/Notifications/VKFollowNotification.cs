using Newtonsoft.Json;
using OneVK.Response;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение об одном или нескольких новых подписчиках.
    /// </summary>
    public class VKFollowNotification : VKNotificationBase,
        INotificationFeedback<VKCountedItemsObject<VKNotificationActionObjectsContainer>>
    {
        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        [JsonProperty("feedback")]
        public VKCountedItemsObject<VKNotificationActionObjectsContainer> Feedback { get; set; }
    }
}
