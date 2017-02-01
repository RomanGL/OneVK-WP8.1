using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Response;
using OneVK.ViewModel.Messages;
using OneVK.Service.Messages;
#if ONEVK_CORE
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
#endif

namespace OneVK.Service
{
    /// <summary>
    /// Сервис прямой авторизации ВКонтакте.
    /// </summary>
    public class VKLoginService
    {
		private const string AuthorizationURL = "https://oauth.vk.com/authorize";
		private const string RedirectURL = "https://oauth.vk.com/blank.html";
		private const string GrantType = "password";
		private const string DisplayType = "page";
		
#if ONEVK_CORE
        private const long ClientID = 0; // TODO.
        private const string ClientSecret = null; // TODO.
        private const string Scope = "audio,friends,docs,groups,messages,notes,notifications,notify,offline,pages,photos,stats,status,video,wall";
#else
        private const string FullScope = "audio,friends,docs,groups,messages,notes,notifications,notify,offline,pages,photos,stats,status,video,wall";
        private readonly long ClientID;
        private readonly string ClientSecret;
        private readonly string Scope;
#endif

#if !ONEVK_CORE
        /// <summary>
        /// Инициализирует новый экземпляр сервиса авторизации.
        /// </summary>
        /// <param name="appID">Идентификатор приложения ВКонтакте.</param>
        /// <param name="clientSecret">Секретный ключ приложения.</param>
        /// <param name="scope">Перечень разрешений.</param>
        internal VKLoginService(long appID, string clientSecret, string scope)
        {
            ClientID = appID;
            ClientSecret = clientSecret;
            Scope = String.IsNullOrWhiteSpace(scope) ? FullScope : scope;
        }

        /// <summary>
        /// Возвращает текущий перечень разрешений.
        /// </summary>
        public string GetCurrentScope() { return Scope; }
#endif

        /// <summary>
        /// Запускает процесс авторизации в социальной сети ВКонтакте.
        /// </summary>
        /// <param name="userName">Имя пользователя, email или номер телефона.</param>
        /// <param name="password">Пароль.</param>
        /// <exception cref="ArgumentException"></exception>
        public void LogIn(string userName, string password)
        {
            if (ClientID == 0)
                throw  new InvalidOperationException("ClientId is 0");
            if (String.IsNullOrEmpty(ClientSecret))
                throw new InvalidOperationException("ClientSecret is empty");

            if (String.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("userName", "Логин не может быть пустым.");
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("password", "Пароль не может быть пустым.");

            var parameters = new Dictionary<string, string>();
            parameters["grant_type"] = GrantType;
            parameters["client_id"] = ClientID.ToString();
            parameters["client_secret"] = ClientSecret;
            parameters["username"] = userName;
            parameters["password"] = password;
            parameters["scope"] = Scope;
            parameters["v"] = VKMethodsConstants.ApiVersion;

#if ONEVK_CORE
            VKHelper.GetLoginResponse(parameters,
                (response) =>
                {
                    if (response.ErrorName == AppConstants.NeedValidation)
                        Messenger.Default.Send<LoginMessage>(
                            new LoginMessage() { State = VKLoginStates.NeedValidation, RedirectURL = response.RedirectURL });
                    else if (response.ErrorName == AppConstants.InvalidClient)
                        Messenger.Default.Send<LoginMessage>(new LoginMessage() { State = VKLoginStates.InvalidClient });
                    else if (response.ErrorName == AppConstants.CaptchaCanceled)
                        Messenger.Default.Send<LoginMessage>(new LoginMessage() { State = VKLoginStates.Nothing });
                    else if (response.GetAccessToken != null)
                    {
                        ServiceLocator.Current.GetInstance<SettingsService>().AccessToken = response.GetAccessToken;
                        Messenger.Default.Send<LoginMessage>(new LoginMessage() { State = VKLoginStates.Login });
                        ServiceHelper.VKLongPollService.StopLongPolling();
                    }
                    else
                        Messenger.Default.Send<LoginMessage>(new LoginMessage() { State = VKLoginStates.UnknownError });
                });
#endif
        }

        /// <summary>
        /// Выполнить авторизацию ВКонтакте.
        /// </summary>
        /// <param name="response">Данные авторизации после валидации.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CompleteLogIn(VKLoginResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response", "Переменная должна быть инициализирована.");

            if (response.ErrorName == AppConstants.ValidationCanceled)
            {
#if ONEVK_CORE
                Messenger.Default.Send(new LoginMessage() { State = VKLoginStates.ValidationCanceled });
#endif
                return;
            }

#if ONEVK_CORE
            ServiceLocator.Current.GetInstance<SettingsService>().AccessToken = response.GetAccessToken;
            //Messenger.Default.Send<LoginMessage>(new LoginMessage() { State = VKLoginStates.Login });
            ServiceHelper.VKLongPollService.StartLongPolling();
            ServiceLocator.Current.GetInstance<IAppMessagesService>().StartAndRestore();
            ServiceLocator.Current.GetInstance<IPromoService>().Update();
#endif
        }

		/// <summary>
		/// Возвращает ссылку для прохождения браузерной авторизации.
		/// </summary>
		public string GetOAuthURL()
        {
            if (ClientID == 0)
                throw new InvalidOperationException("ClientId is 0");
            if (String.IsNullOrEmpty(ClientSecret))
                throw new InvalidOperationException("ClientSecret is empty");

            return String.Format("{0}?client_id={1}&scope={2}&redirect_uri={3}&display={4}&v={5}&response_type=token",
                AuthorizationURL, ClientID, Scope, RedirectURL, DisplayType, VKMethodsConstants.ApiVersion);
        }
    }
}
