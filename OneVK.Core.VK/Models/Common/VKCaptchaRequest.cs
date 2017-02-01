namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет запрос каптчи ВКонтакте.
    /// </summary>
    public sealed class VKCaptchaRequest
    {
        /// <summary>
        /// Возвращает идентификатор каптчи.
        /// </summary>
        public string CaptchaSid { get; private set; }
        /// <summary>
        /// Возвращает ссылку на изображение каптчи.
        /// </summary>
        public string CaptchaImg { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VKCaptchaRequest"/> с заданными параметрами запроса каптчи.
        /// </summary>
        /// <param name="captchaSid">Идентификатор каптчи.</param>
        /// <param name="captchaImg">Ссылка на изображение каптчи.</param>
        internal VKCaptchaRequest(string captchaSid, string captchaImg)
        {
            CaptchaSid = captchaSid;
            CaptchaImg = captchaImg;
        }
    }
}
