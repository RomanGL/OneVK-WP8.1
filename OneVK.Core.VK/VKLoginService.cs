using OneVK.Core.Services;
using OneVK.Core.VK.Models.Common;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Authentication.Web;

namespace OneVK.Core.VK
{
    /// <summary>
    /// Представляет сервис авторизации ВКонтакте.
    /// </summary>
    public sealed class VKLoginService : IVKLoginService
    {
        public const string ACCESS_TOKEN_PARAMETER = "AccessToken";
        private const string AUTHORIZATION_URL = "https://oauth.vk.com/authorize";
        private const string REDIRECT_URL = "https://oauth.vk.com/blank.html";
        private const string PARAMETERS_MASK = "{0}?client_id={1}&scope={2}&redirect_uri={3}&display=popup&v={4}&response_type=token";
        private const string SCOPE = "audio,friends,docs,groups,offline,status,video,wall";        

        private const string CLIENT_ID = null; // TODO.

        private ISettingsService settingsService;
        private IDialogService dialogService;

        private VKAccessToken AccessToken { get { return settingsService.Get<VKAccessToken>(ACCESS_TOKEN_PARAMETER); } }

        /// <summary>
        /// Просиходит при успешной авторизации пользователя.
        /// </summary>
        public event TypedEventHandler<IVKLoginService, EventArgs> UserLogin;
        /// <summary>
        /// Происходит при выходе пользователя.
        /// </summary>
        public event TypedEventHandler<IVKLoginService, EventArgs> UserLogout;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VKLoginService"/>.
        /// </summary>
        /// <param name="settingsService">Сервис настроек.</param>
        /// <param name="dialogService">Сервис отображения диалоговых окон.</param>
        public VKLoginService(ISettingsService settingsService, IDialogService dialogService)
        {
            this.settingsService = settingsService;
            this.dialogService = dialogService;
        }

        /// <summary>
        /// Возвращает идентификатор текущего авторизованного пользователя.
        /// </summary>
        public long UserID
        {
            get
            {
                if (AccessToken == null)
                    throw new InvalidOperationException("Авторизация не выполнена.");
                return AccessToken.UserID;
            }
        }

        /// <summary>
        /// Возвращает токен авторизованного пользователя.
        /// </summary>
        public string Token
        {
            get
            {
                if (AccessToken == null)
                    throw new InvalidOperationException("Авторизация не выполнена.");
                return AccessToken.AccessToken;
            }
        }

        /// <summary>
        /// Возвращает значение, выполнена ли авторизация.
        /// </summary>
        public bool IsAuthorized { get { return AccessToken != null; } }

        /// <summary>
        /// Выполнить авторизацию ВКонтакте.
        /// </summary>
        public async Task Login()
        {
            if (String.IsNullOrEmpty(CLIENT_ID))
                throw new InvalidOperationException("CLIENT_ID is empty");

            var authUri = new Uri(String.Format(PARAMETERS_MASK, AUTHORIZATION_URL, CLIENT_ID, SCOPE, REDIRECT_URL, Constants.API_VERSION));
            var redirectUri = new Uri(REDIRECT_URL);

            try
            {
                var authResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, authUri, redirectUri);

                if (authResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    dialogService.Show(String.Format("Возможно на сервере неполадки. Повторите попытку позже. Код ошибки: {0}",
                        authResult.ResponseErrorDetail.ToString()),
                        "Не удалось выполнить авторизацию");
                }
                else if (authResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    var response = authResult.ResponseData.Split(new char[] { '#' })[1].Split(new char[] { '&' });
                    string token = response[0].Split('=')[1];
                    long userID = long.Parse(response[2].Split('=')[1]);
                    
                    settingsService.Set(ACCESS_TOKEN_PARAMETER, new VKAccessToken { AccessToken = token, UserID = userID });
                }
            }
            catch (Exception)
            {
                dialogService.Show("Отсутствует подключение к сети. Проверьте настройки передачи данных и повторите попытку.",
                    "Не удалось выполнить авторизацию");          
            }          
        }

        /// <summary>
        /// Выполняет авторизацию по указанному токену.
        /// </summary>
        /// <param name="token">Токен авторизации.</param>
        public void Login(VKAccessToken token)
        {
            settingsService.Set(ACCESS_TOKEN_PARAMETER, token);

            if (UserLogin != null)
                UserLogin(this, EventArgs.Empty);
        }

        /// <summary>
        /// Отменить авторизацию ВКонтакте.
        /// </summary>
        public void Logout()
        {
            settingsService.Remove(ACCESS_TOKEN_PARAMETER);

            if (UserLogout != null)
                UserLogout(this, EventArgs.Empty);
        }
    }
}
