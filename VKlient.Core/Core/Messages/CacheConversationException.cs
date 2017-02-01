using System;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Исключение, которое выбрасывается при возникновении ошибки при кэшировании беседы.
    /// </summary>
    public class CacheConversationException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CacheConversationException"/>.
        /// </summary>
        public CacheConversationException() { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CacheConversationException"/> с
        /// заданным описанием ошибки.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public CacheConversationException(string message) : base(message) { }
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CacheConversationException"/> с
        /// заданным описание ошибки и ссылкой на исключение, которое стало причиной данного исключения.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="inner">Исключение, которое стало причиной данного.</param>
        public CacheConversationException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Беседа, которую не удалось кэшировать.
        /// </summary>
        public IConversation Conversation { get; set; }
    }
}
