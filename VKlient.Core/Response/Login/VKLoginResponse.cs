using Newtonsoft.Json;
using System;
using OneVK.Model.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой ответ сервера на запрос прямой авторизации.
    /// </summary>
    public class VKLoginResponse
    {
        /// <summary>
        /// Название ошибки.
        /// </summary>
        [JsonProperty("error")]
        public string ErrorName { get; set; }
        /// <summary>
        /// Ссылка для валидации.
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectURL { get; set; }

        /// <summary>
        /// Возвращает объект ключа доступа.
        /// </summary>
        public VKAccessToken GetAccessToken
        {
            get 
            { 
                if (!String.IsNullOrEmpty(AccessToken) && UserID != 0)
                    return new VKAccessToken() { AccessToken = AccessToken, ExpiresIn = ExpiresIn, UserID = UserID };
                return null;
            }
        }
        /// <summary>
        /// Возвращает объект запроса каптчи.
        /// </summary>
        public VKCaptchaRequest GetCaptcha
        {
            get { return new VKCaptchaRequest() { CaptchaSid = CaptchaSid, CaptchaURL = CaptchaURL }; }
        }

        /// <summary>
        /// Ключ доступа.
        /// </summary>
        [JsonProperty("access_token")]
        private string AccessToken { get; set; }
        /// <summary>
        /// Срок жизник ключа в секундах.
        /// </summary>
        [JsonProperty("expires")]
        private int ExpiresIn { get; set; }
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [JsonProperty("user_id")]
        private ulong UserID { get; set; }
        /// <summary>
        /// Идентификатор каптчи.
        /// </summary>
        [JsonProperty("captcha_sid")]
        private string CaptchaSid { get; set; }
        /// <summary>
        /// Ссылка на картинку каптчи.
        /// </summary>
        [JsonProperty("captcha_img")]
        private string CaptchaURL { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным токеном доступа 
        /// и идентификатором пользователя.
        /// </summary>
        /// <param name="accessToken">Токен доступа.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        public VKLoginResponse(string accessToken, ulong userID)
        {
            AccessToken = accessToken;
            UserID = userID;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public VKLoginResponse() { }
    }
}
