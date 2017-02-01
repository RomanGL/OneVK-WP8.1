namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет информацию о LongPoll событии удаления сообщения.
    /// </summary>
    public sealed class MessageDeletedUpdate : IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public VKLongPollUpdateType Type { get { return VKLongPollUpdateType.MessageDeleted; } }

        /// <summary>
        /// Идентификатор сообщения.
        /// </summary>
        public ulong MessageID { get; internal set; }

        internal MessageDeletedUpdate() { }
    }
}
