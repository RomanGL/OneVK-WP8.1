using System;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Исключение, которое выбрасывается при возникновении ошибки при кэшировании списка диалогов.
    /// </summary>
    public class CacheDialogsException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CacheDialogsException"/>.
        /// </summary>
        public CacheDialogsException() { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CacheDialogsException"/> с
        /// заданным описанием ошибки.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public CacheDialogsException(string message) : base(message) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CacheDialogsException"/> с
        /// заданным описание ошибки и ссылкой на исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="inner">Исключение, которое стало причиной данного.</param>
        public CacheDialogsException(string message, Exception inner) : base(message, inner) { }
    }
}
