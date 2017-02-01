using OneVK.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы ответов.
    /// </summary>
    public class NotificationsViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="NotificationsViewModel"/>.
        /// </summary>
        public NotificationsViewModel()
        {
        }
        #endregion

        #region Приватные поля
        private NotificationsCollection _notifications;
        #endregion

        #region Свойства
        /// <summary>
        /// Возвращает коллекцию уведомлений.
        /// </summary>
        public NotificationsCollection Notifications
        {
            get { return _notifications; }
            private set { Set(() => Notifications, ref _notifications, value); }
        }
        #endregion

        #region Команды
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override void Activate(NavigationMode mode = NavigationMode.New)
        {
            if (Notifications == null)
                Notifications = new NotificationsCollection();
        }

        /// <summary>
        /// Деактивирует модель представления.
        /// </summary>
        public override void Deactivate(NavigationMode mode = NavigationMode.New)
        {
            
        }
        #endregion

        #region Приватные методы
        #endregion
    }
}
