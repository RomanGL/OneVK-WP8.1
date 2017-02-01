using System;

namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет исключение, которое выбрасывается при ошибке веб-соединения.
    /// </summary>
    internal sealed class WebConnectionException : Exception
    {
        /// <summary>
        /// Инииализирует новый экемпляр класса <see cref="WebConnectionException"/>.
        /// </summary>
        public WebConnectionException() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WebConnectionException"/>.
        /// </summary>
        /// <param name="message">Текст ошибки.</param>
        public WebConnectionException(string message)
            : base(message)
        { }
    }
}
