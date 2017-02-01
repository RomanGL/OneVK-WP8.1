namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой запрос каптчи.
    /// </summary>
    public sealed class VKCaptchaRequest
    {
        /// <summary>
        /// Идентификатор каптчи.
        /// </summary>
        public string CaptchaSid { get; internal set; }
        /// <summary>
        /// Ссылка на картинку каптчи.
        /// </summary>
        public string CaptchaURL { get; internal set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal VKCaptchaRequest() { }
    }
}
