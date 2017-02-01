namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой ответ на запрос каптчи ВКонтакте.
    /// </summary>
    public sealed class VKCaptchaResponse
    {
        /// <summary>
        /// Объект с информацией о запрошенной каптче.
        /// </summary>
        public VKCaptchaRequest Request { get; private set; }
        /// <summary>
        /// Отменен ли ввода каптчи.
        /// </summary>
        public bool IsCanceled { get; set; }
        /// <summary>
        /// Строка ответа пользователя.
        /// </summary>
        public string UserResponse { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным объектом запрошенной каптчи.
        /// </summary>
        /// <param name="request">Объект с информацией о запрошенной каптче.</param>
        public VKCaptchaResponse(VKCaptchaRequest request)
        {
            Request = request;
        }
    }
}
