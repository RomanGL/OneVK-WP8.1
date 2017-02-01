namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Информация о LongPoll обновлении, происходящем при старте набора пользователем
    /// сообщения в диалоге.
    /// </summary>
    public class UserIsTypingInDialogUpdate : IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public virtual VKLongPollUpdateType Type { get { return VKLongPollUpdateType.UserIsTypingInDialog; } }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public long UserID { get; internal set; }

        internal UserIsTypingInDialogUpdate() { }
    }
}
