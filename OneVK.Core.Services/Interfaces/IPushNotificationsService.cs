using System.Threading.Tasks;
using Windows.Foundation;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис работы с Push-уведомлениями.
    /// </summary>
    public interface IPushNotificationsService
    {
        /// <summary>
        /// Регистрирует устройство на получение Push-уведомлений.
        /// </summary>
        Task RegisterDevice();

        /// <summary>
        /// Отписывает устройство от получения Push-уведомлений
        /// </summary>
        Task UnregisterDevice();
    }
}
