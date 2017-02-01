using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Model.Video;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет видеозапись оповещения.
    /// </summary>
    public class VKNotificationVideo : VKVideoBase, IActionObjectContainer
    {
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
