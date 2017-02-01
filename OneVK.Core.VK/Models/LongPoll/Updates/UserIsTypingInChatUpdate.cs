namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Информация о LongPoll обновлении, происходящем при старте набора пользователем
    /// сообщения в чате.
    /// </summary>
    public sealed class UserIsTypingInChatUpdate : UserIsTypingInDialogUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public override VKLongPollUpdateType Type { get { return VKLongPollUpdateType.UserIsTypingInChat; } }

        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        public uint ChatID { get; internal set; }

        internal UserIsTypingInChatUpdate() { }
    }
}
