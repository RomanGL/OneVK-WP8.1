using Newtonsoft.Json;
using OneVK.Enums.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой ошибку ВКонтакте.
    /// </summary>
    public sealed class VKError
    {
        /// <summary>
        /// Тип ошибки. 
        /// Имеет значение None, если ошибки не произошло. 
        /// </summary>
        [JsonProperty("error_code")]
        public VKErrors ErrorType { get; set; }
        /// <summary>
        /// Идентификатор каптчи.
        /// </summary>
        [JsonProperty("captcha_sid")]
        string CaptchaSid { get; set; }
        /// <summary>
        /// Ссылка на картинку с каптчей.
        /// </summary>
        [JsonProperty("captcha_img")]
        string CaptchaURL { get; set; }

        /// <summary>
        /// Возвращает объект каптчи.
        /// </summary>
        public VKCaptchaRequest Captcha
        { 
            get 
            { 
                return new VKCaptchaRequest() { CaptchaSid = CaptchaSid, CaptchaURL = CaptchaURL }; 
            } 
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal VKError() { }
    }
}
