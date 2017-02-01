using System;

namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет информацию о LongPoll событии получения нового сообщения.
    /// </summary>
    public sealed class MessageReceivedUpdate : IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public VKLongPollUpdateType Type { get { return VKLongPollUpdateType.MessageReceived; } }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public ulong MessageID { get; internal set; }
        /// <summary>
        /// Флаги сообщения.
        /// </summary>
        public VKMessageFlags Flags { get; internal set; }
        /// <summary>
        /// Идентификатор автора сообщения.
        /// </summary>
        public long UserID { get; internal set; }
        /// <summary>
        /// Идентификатор чата, в котором отправлено сообщение.
        /// </summary>
        public uint ChatID { get; internal set; }
        /// <summary>
        /// Является ли сообщением из чата.
        /// </summary>
        public bool IsChatMessage { get { return UserID != 0; } }
        /// <summary>
        /// Время отправки сообщения.
        /// </summary>
        public DateTime Timestamp { get; internal set; }
        /// <summary>
        /// Тема сообщения.
        /// </summary>
        public string Subject { get; internal set; }
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Text { get; internal set; }

        internal MessageReceivedUpdate() { }
    }
}
