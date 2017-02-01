using OneVK.Enums.LongPoll;
using System;

namespace OneVK.Model.LongPoll
{
    /// <summary>
    /// Содержит информацию о событии добавления нового сообщения.
    /// </summary>
    public sealed class MessageInfo
    {
        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public ulong MessageID { get; internal set; }
        /// <summary>
        /// Установленные флаги сообщения.
        /// </summary>
        public VKMessageFlags Flags { get; internal set; }
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; internal set; }
        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        public uint ChatID { get; internal set; }
        /// <summary>
        /// Является ли сообщением из чата.
        /// </summary>
        public bool IsChatMessage { get { return UserID == 0; } }
        /// <summary>
        /// Время отправки сообщения.
        /// </summary>
        public DateTime Timestamp { get; internal set; }
        /// <summary>
        /// Заголовок сообщения.
        /// </summary>
        public string Subject { get; internal set; }
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal MessageInfo() { }
    }
}
