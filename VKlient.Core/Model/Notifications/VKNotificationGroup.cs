using Newtonsoft.Json;
using OneVK.Enums.Notifications;
using OneVK.Model.Group;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет сообщество уведомления.
    /// </summary>
    public class VKNotificationGroup : VKGroupBase, INotificationActionObject
    {
        /// <summary>
        /// Тип объекта действия.
        /// </summary>
        [JsonProperty("type")]
        public ActionObjectType Type { get { return ActionObjectType.Group; } }
    }
}
