using Newtonsoft.Json;
using OneVK.Response;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение об одном или нескольких новых отметках Мне нравится к фотографии пользователя.
    /// </summary>
    public class VKLikePhotoNotification : VKNotificationBase,
        INotificationParent<VKNotificationPhoto>,
        INotificationFeedback<VKCountedItemsObject<VKNotificationActionObjectsContainer>>
    {
        /// <summary>
        /// Пост, у которого появились новые отметки Мне нравится.
        /// </summary>
        [JsonProperty("parent")]
        public VKNotificationPhoto Parent { get; set; }

        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        [JsonProperty("feedback")]
        public VKCountedItemsObject<VKNotificationActionObjectsContainer> Feedback { get; set; }
    }
}
