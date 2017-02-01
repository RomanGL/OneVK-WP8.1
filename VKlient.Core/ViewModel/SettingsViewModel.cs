using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.ViewModel.Messages;
using OneVK.Model.Photo;
using OneVK.Service;

namespace OneVK.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public SettingsViewModel()
        {
            GoToProfileCommand = new RelayCommand(() => 
                NavigationHelper.Navigate(AppViews.ProfileView, ServiceHelper.SettingsService.AccessToken.UserID));
            GoToCommonSettingsCommand = new RelayCommand(() => NavigationHelper.Navigate(AppViews.CommonSettingsView));
            GoToNotificationsSettingsCommand = new RelayCommand(() => NavigationHelper.Navigate(AppViews.NotificationsSettingsView));
            GoToAboutCommand = new RelayCommand(() => NavigationHelper.Navigate(AppViews.AboutView));
        }
        #endregion

        #region Приватные поля
        #endregion

        #region Свойства
        #endregion

        #region Команды
        /// <summary>
        /// Команда перехода к профилю текущего пользователя.
        /// </summary>
        public RelayCommand GoToProfileCommand { get; private set; }
        /// <summary>
        /// Команда перехода к странице настроек уведомлений.
        /// </summary>
        public RelayCommand GoToNotificationsSettingsCommand { get; private set; }
        /// <summary>
        /// Команда перехода к общим настройкам.
        /// </summary>
        public RelayCommand GoToCommonSettingsCommand { get; private set; }
        /// <summary>
        /// Команда перехода в раздел о программе.
        /// </summary>
        public RelayCommand GoToAboutCommand { get; private set; }
        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion
    }
}
