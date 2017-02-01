using System;

namespace OneVK.Core
{
    /// <summary>
    /// Исключение, которое выбрасывается при возникновении ошибки необходимости авторизации.
    /// </summary>
    public class AuthorizationRequiredException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorizationRequiredException"/>.
        /// </summary>
        public AuthorizationRequiredException() { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorizationRequiredException"/> с
        /// заданным описанием ошибки.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public AuthorizationRequiredException(string message) : base(message) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorizationRequiredException"/> с
        /// заданным описание ошибки и ссылкой на исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="inner">Исключение, которое стало причиной данного.</param>
        public AuthorizationRequiredException(string message, Exception inner) : base(message, inner) { }
    }
}
