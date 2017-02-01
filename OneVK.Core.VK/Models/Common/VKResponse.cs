using Newtonsoft.Json;
using System;
using OneVK.Core.VK.Json;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет ответ сервера ВКонтакте на запрос.
    /// </summary>
    /// <typeparam name="T">Тип результирующих данных.</typeparam>
    public sealed class VKResponse<T>
    {
        [JsonProperty("captcha_sid")]
        private string CaptchaSid { get; set; }

        [JsonProperty("captcha_img")]
        private string CaptchaImg { get; set; }

        /// <summary>
        /// Возвращает тип ошибки ВКонтакте.
        /// </summary>
        [JsonProperty("error")]
        [JsonConverter(typeof(VKResponseErrorConverter))]
        public VKErrors Error { get; set; }

        /// <summary>
        /// Возвращает значение, успешно ли выполнен запрос.
        /// </summary>
        [JsonIgnore]
        public bool IsSuccess { get { return Error == VKErrors.None; } }

        /// <summary>
        /// Возвращает каптчу или null, если запроса не получено.
        /// </summary>
        [JsonIgnore]
        public VKCaptchaRequest GetCaptcha
        {
            get
            {
                if (String.IsNullOrEmpty(CaptchaSid) || String.IsNullOrEmpty(CaptchaImg))
                    return null;
                return new VKCaptchaRequest(CaptchaSid, CaptchaImg);
            }
        }

        /// <summary>
        /// Возвращает содержимое ответа.
        /// </summary>
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}
