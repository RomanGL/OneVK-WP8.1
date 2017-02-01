using OneVK.Enums.LongPoll;

namespace OneVK.Model.LongPoll
{
    /// <summary>
    /// Содержит информацию о событии действия с флагами сообщений.
    /// </summary>
    public sealed class MessageFlagsInfo
    {
        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public ulong MessageID { get; internal set; }

        /// <summary>
        /// Новые флаги сообщения.
        /// </summary>
        public VKMessageFlags Flags { get; internal set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        internal MessageFlagsInfo() { }
    }
}
