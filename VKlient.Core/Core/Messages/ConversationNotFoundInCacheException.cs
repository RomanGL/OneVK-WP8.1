using System;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Исключение, которое выбрасывается при запросе отсутствующей в кэше беседы.
    /// </summary>
    public class ConversationNotFoundInCacheException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConversationNotFoundInCacheException"/>.
        /// </summary>
        public ConversationNotFoundInCacheException() { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConversationNotFoundInCacheException"/> с
        /// заданным описанием ошибки.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public ConversationNotFoundInCacheException(string message) : base(message) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConversationNotFoundInCacheException"/> с
        /// заданным описание ошибки и ссылкой на исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="inner">Исключение, которое стало причиной данного.</param>
        public ConversationNotFoundInCacheException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Идентификатор запрошенной беседы.
        /// </summary>
        public long ConversationID { get; set; }
    }
}
