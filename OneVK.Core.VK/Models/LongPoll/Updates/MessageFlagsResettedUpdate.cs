namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет данные о LongPoll событии сброса флагов сообщения.
    /// </summary>
    public sealed class MessageFlagsResettedUpdate : MessageFlagsSettedUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public override VKLongPollUpdateType Type { get { return VKLongPollUpdateType.MessageFlagsResetted; } }

        internal MessageFlagsResettedUpdate() { }
    }
}
