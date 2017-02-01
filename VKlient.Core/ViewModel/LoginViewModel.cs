using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OneVK.ViewModel.Messages;
using OneVK.Enums.Common;
using OneVK.Service;
using Windows.System;
using OneVK.Enums.App;
using Windows.Storage;
using System.Runtime.Serialization;
using OneVK.Core;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Модель представления страницы авторизации.
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Приватные поля
        private string _login;
        private string _password;
        private bool _isWorking;
        #endregion

        #region Свойства
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login
        {
            get { return _login; }
            set
            {
                if (!Set<string>(() => Login, ref _login, value))
                    return;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (!Set<string>(() => Password, ref _password, value))
                    return;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
        /// <summary>
        /// Доступен ли интерфейс пользователя для взаимодействия.
        /// </summary>
        public bool IsEnabled
        {
            get { return _isWorking ? false : true; }
        }
        #endregion

        #region Команды
        /// <summary>
        /// Команда, запускающая процесс авторизации.
        /// </summary>
        public RelayCommand LoginCommand { get; private set; }
        /// <summary>
        /// Команда, запускающая процесс регистрации.
        /// </summary>
        public RelayCommand JoinCommand { get; private set; }
        /// <summary>
        /// Команда, запускающая процесс восстановления пароля.
        /// </summary>
        public RelayCommand RestoreCommand { get; private set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public LoginViewModel()
        {
            InitializeCommands();
        }
        #endregion

        #region Активация/деактивация VM 
        /// <summary>
        /// Активация модели представления и подписка на сообщения.
        /// </summary>
        public override void Activate(NavigationMode mode = NavigationMode.New)
        {
            Messenger.Default.Register<LoginMessage>(this, OnLogin);
        }

        /// <summary>
        /// Деактивация модели представления и отписка от сообщений.
        /// </summary>
        public override void Deactivate(NavigationMode mode = NavigationMode.New)
        {
            Messenger.Default.Unregister<LoginMessage>(this);
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Инициализирует комманды при первом использовании.
        /// </summary>
        private void InitializeCommands()
        {
            LoginCommand = new RelayCommand(
                () => DoLogin(),
                () =>
                {
                    return String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password) ?
                        false : true;
                });
            JoinCommand = new RelayCommand(
                async () =>
                {
                    await Launcher.LaunchUriAsync(new Uri("https://vk.com/join"));
                });
            RestoreCommand = new RelayCommand(
                async () =>
                {
                    await Launcher.LaunchUriAsync(new Uri("https://vk.com/restore"));
                });
        }

        /// <summary>
        /// Выполняет авторизацию во ВКонтакте.
        /// </summary>
        private void DoLogin()
        {
            ServiceLocator.Current.GetInstance<VKLoginService>().LogIn(Login, Password);
            _isWorking = true;
            RaisePropertyChanged(() => IsEnabled);            
        }

        /// <summary>
        /// Происходит при получении сообщения авторизации.
        /// </summary>
        /// <param name="message">Объект сообщения.</param>
        private async void OnLogin(LoginMessage message)
        {
            switch (message.State)
            {
                case VKLoginStates.Login:
                    Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage()
                    {
                        Operation = NavigationType.NewClear,
                        Page = AppViews.NewsView
                    });
                    break;
                case VKLoginStates.InvalidClient:
                    await ServiceLocator.Current.GetInstance<IDialogService>()
                        .ShowMessage("Неверное имя пользователя или пароль.", "Ошибка авторизации");
                    break;
                case VKLoginStates.NeedValidation:
                    break;
                case VKLoginStates.ValidationCanceled:
                    break;
                case VKLoginStates.ConnectionError:
                    await ServiceLocator.Current.GetInstance<IDialogService>()
                        .ShowMessage("Не удалось подключиться к ВКонтакте. Проверьте настройки передачи данных и повторите попытку.",
                    "Ошибка соединения");
                    break;
                case VKLoginStates.UnknownError:
                    await ServiceLocator.Current.GetInstance<IDialogService>()
                        .ShowMessage("Не удалось выполнить авторизацию. Повторите попытку позже.",
                    "Неизвестная ошибка");
                    break;
            }
            _isWorking = false;
            RaisePropertyChanged(() => IsEnabled);
        }
        #endregion
    }
}
