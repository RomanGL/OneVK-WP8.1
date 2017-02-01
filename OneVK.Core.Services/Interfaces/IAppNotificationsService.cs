using OneVK.Core.Models.AppNotifications;
using Windows.Foundation;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис для отправки уведомлений внутри приложения.
    /// </summary>
    public interface IAppNotificationsService
    {
        /// <summary>
        /// Происходит при получении нового уведомления для отображения.
        /// </summary>
        event TypedEventHandler<IAppNotificationsService, AppNotification> NewNotification;

        /// <summary>
        /// Отправить уведомление.
        /// </summary>
        /// <param name="notification">Данные уведомления.</param>
        void SendNotification(AppNotification notification);
    }
}
