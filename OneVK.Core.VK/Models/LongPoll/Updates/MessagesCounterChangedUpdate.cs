namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет информацию о LongPoll событии изменения счетчика непрочитанных сообщений.
    /// </summary>
    public sealed class MessagesCounterChangedUpdate : IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public VKLongPollUpdateType Type { get { return VKLongPollUpdateType.MessagesCounterChanged; } }

        /// <summary>
        /// Количество непрочитанных сообщений.
        /// </summary>
        public uint Number { get; internal set; }

        internal MessagesCounterChangedUpdate() { }
    }
}
