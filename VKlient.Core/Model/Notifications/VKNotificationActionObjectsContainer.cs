using Newtonsoft.Json;
using OneVK.Core.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет контейнер информации о пользователе.
    /// </summary>
    public class VKNotificationActionObjectsContainer : IActionObjectContainer
    {
        /// <summary>
        /// Идентификатор объекта-инициатора.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; }

        /// <summary>
        /// Объект-инициатор оповещения.
        /// </summary>
        [JsonProperty("action_object")]
        [JsonConverter(typeof(NotificationActionObjectConverter))]
        public INotificationActionObject ActionObject { get; set; }
    }
}
