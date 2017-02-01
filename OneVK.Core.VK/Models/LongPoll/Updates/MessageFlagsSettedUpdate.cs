namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет информацию о LongPoll событии устновки флагов сообщения.
    /// </summary>
    public class MessageFlagsSettedUpdate : IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public virtual VKLongPollUpdateType Type { get { return VKLongPollUpdateType.MessageFlagsSetted; } }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public ulong MessageID { get; internal set; }

        /// <summary>
        /// Маска флагов
        /// </summary>
        public VKMessageFlags Mask { get; internal set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public long UserID { get; internal set; }

        internal MessageFlagsSettedUpdate() { }
    }
}
