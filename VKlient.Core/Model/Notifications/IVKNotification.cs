using OneVK.Enums.Notifications;
using System;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение ВКонтакте.
    /// </summary>
    public interface IVKNotification
    {
        /// <summary>
        /// Тип оповещения ВКонтакте.
        /// </summary>
        VKNotificationType Type { get; set; }

        /// <summary>
        /// Время появления ответа.
        /// </summary>
        DateTime Date { get; set; }
    }
}
