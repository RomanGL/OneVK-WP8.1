using System;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Исключение, которое выбрасывается при запросе кэша сообщений, тогда как он пуст.
    /// </summary>
    public class NoDialogsInCacheException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NoDialogsInCacheException"/>.
        /// </summary>
        public NoDialogsInCacheException() { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NoDialogsInCacheException"/> с
        /// заданным описанием ошибки.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public NoDialogsInCacheException(string message) : base(message) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NoDialogsInCacheException"/> с
        /// заданным описание ошибки и ссылкой на исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="inner">Исключение, которое стало причиной данного.</param>
        public NoDialogsInCacheException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Идентификатор запрошенной беседы.
        /// </summary>
        public long ConversationID { get; set; }
    }
}
