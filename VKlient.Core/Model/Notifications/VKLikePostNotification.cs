using Newtonsoft.Json;
using OneVK.Response;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение об одном или нескольких новых отметках Мне нравится к посту пользователя.
    /// </summary>
    public class VKLikePostNotification : VKNotificationBase,
        INotificationParent<VKNotificationPost>,
        INotificationFeedback<VKCountedItemsObject<VKNotificationActionObjectsContainer>>
    {
        /// <summary>
        /// Пост, у которого появились новые отметки Мне нравится.
        /// </summary>
        [JsonProperty("parent")]
        public VKNotificationPost Parent { get; set; }

        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        [JsonProperty("feedback")]
        public VKCountedItemsObject<VKNotificationActionObjectsContainer> Feedback { get; set; }
    }
}
