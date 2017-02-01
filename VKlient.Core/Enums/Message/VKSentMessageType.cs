namespace OneVK.Enums.Message
{
    /// <summary>
    /// Перечисление состояний исходящих сообщений.
    /// </summary>
    public enum VKSentMessageType : byte
    {        
        /// <summary>
        /// Сообщение не прочитано. 
        /// </summary>
        Unread = 0,
        /// <summary>
        /// Сообщение прочитано.
        /// </summary>
        Read,
        /// <summary>
        /// Сообщение отправляется.
        /// </summary>
        Sending,
        /// <summary>
        /// Ошибка при отправке.
        /// </summary>
        Error
    }
}
