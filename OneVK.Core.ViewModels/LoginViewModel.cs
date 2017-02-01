using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using OneVK.Core.VK;
using OneVK.Core.VK.Models.Common;
using System;

namespace OneVK.Core.ViewModels
{
    /// <summary>
    /// Представляет модель представления страницы авторизации.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private IVKLoginService loginService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoginViewModel"/>.
        /// </summary>
        /// <param name="navigationService">Сервис навигации.</param>
        /// <param name="loginService">Сервис авторизации ВКонтакте.</param>
        public LoginViewModel(INavigationService navigationService, IVKLoginService loginService,
            IVKLongPollService vkLongPollService)
        {
            this.navigationService = navigationService;
            this.loginService = loginService;

            LoginCommand = new DelegateCommand(OnLoginCommand);
        }

        /// <summary>
        /// Команда для запуска процесса авторизации.
        /// </summary>
        public DelegateCommand LoginCommand { get; private set; }

        /// <summary>
        /// Выполняет авторизацию по токену.
        /// </summary>
        /// <param name="token">Токен авторизации.</param>
        public bool LoginToken(VKAccessToken token)
        {
            loginService.Login(token);
            return CompleteLogin();
        }

        /// <summary>
        /// Вызывается при активации комманды авторизации.
        /// </summary>
        private async void OnLoginCommand()
        {
            await loginService.Login();
            CompleteLogin();
        }

        /// <summary>
        /// Завершает процесс авторизации.
        /// </summary>
        private bool CompleteLogin()
        {
            if (loginService.IsAuthorized)
            {
                navigationService.Navigate("NewsView", null);
                navigationService.RemoveLastPage();
                return true;
                
            }
            return false;
        }
    }
}
