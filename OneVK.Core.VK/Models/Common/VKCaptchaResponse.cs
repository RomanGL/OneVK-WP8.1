namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет ответ пользователя на запрос каптчи.
    /// </summary>
    public sealed class VKCaptchaResponse
    {
        /// <summary>
        /// Возвращает идентификатор каптчи.
        /// </summary>
        public string CaptchaSid { get; private set; }
        /// <summary>
        /// Возвращает текст, введенный пользователем на запрос каптчи.
        /// </summary>
        public string CaptchaKey { get; private set; }
        /// <summary>
        /// Возвращает значение, нужно ли отменить ввод каптчи.
        /// </summary>
        public bool Cancel { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VKCaptchaResponse"/> с
        /// заданным ответом пользователя на запрос каптчи.
        /// </summary>
        /// <param name="captchaSid">Идентификатор каптчи.</param>
        /// <param name="captchaKey">Ответ пользователя.</param>
        public VKCaptchaResponse(string captchaSid, string captchaKey)
        {
            CaptchaSid = captchaSid;
            CaptchaKey = captchaKey;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="VKCaptchaResponse"/> со
        /// значением отмены ввода каптчи пользователем.
        /// </summary>
        /// <param name="captchaSid">Идентификатор каптчи.</param>
        public VKCaptchaResponse(string captchaSid)
        {
            CaptchaSid = captchaSid;
            Cancel = true;
        }
    }
}
