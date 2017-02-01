using System;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Исключение, которое выбрасывается при возникновении ошибки при очистке кэша.
    /// </summary>
    public class ClearCacheException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClearCacheException"/>.
        /// </summary>
        public ClearCacheException() { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClearCacheException"/> с
        /// заданным описанием ошибки.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public ClearCacheException(string message) : base(message) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClearCacheException"/> с
        /// заданным описание ошибки и ссылкой на исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="inner">Исключение, которое стало причиной данного.</param>
        public ClearCacheException(string message, Exception inner) : base(message, inner) { }
    }
}
