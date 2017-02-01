using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Model.Photo;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет фотографию оповещения.
    /// </summary>
    public class VKNotificationPhoto : VKPhoto, IActionObjectContainer
    {
        /// <summary>
        /// Идентификатор поста.
        /// </summary>
        [JsonProperty("post_id")]
        public ulong PostID { get; set; }

        /// <summary>
        /// Идентификатор владельца фотографии.
        /// </summary>
        [JsonIgnore]
        public long FromID { get { return OwnerID; } }

        /// <summary>
        /// Объект-инициатор оповещения.
        /// </summary>
        [JsonProperty("action_object")]
        [JsonConverter(typeof(NotificationActionObjectConverter))]
        public INotificationActionObject ActionObject { get; set; }
    }
}
