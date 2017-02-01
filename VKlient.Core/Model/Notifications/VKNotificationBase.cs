using System;
using OneVK.Enums.Notifications;
using Newtonsoft.Json;
using OneVK.Core.Json;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет уведомление ВКонтакте.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class VKNotificationBase : IVKNotification
    {
        /// <summary>
        /// Время появления ответа.
        /// </summary>
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Тип уведомления ВКонтакте.
        /// </summary>
        public VKNotificationType Type { get; set; }
    }
}
