namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет информацию о LongPoll событии замены флагов сообщения.
    /// </summary>
    public class MessageFlagsReplacedUpdate : IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public VKLongPollUpdateType Type { get { return VKLongPollUpdateType.MessageFlagsReplaced; } }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public ulong MessageID { get; internal set; }

        /// <summary>
        /// Флаги сообщения.
        /// </summary>
        public VKMessageFlags Flags { get; internal set; }

        internal MessageFlagsReplacedUpdate() { }
    }
}
