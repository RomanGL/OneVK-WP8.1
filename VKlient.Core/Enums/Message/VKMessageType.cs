namespace OneVK.Enums.Message
{
    /// <summary>
    /// Типы сообщений ВКонтакте.
    /// </summary>
    public enum VKMessageType : byte
    {
        /// <summary>
        /// Полученное сообщение.
        /// </summary>
        Received = 0,
        /// <summary>
        /// Отправленное сообщение.
        /// </summary>
        Sent = 1
    }
}
