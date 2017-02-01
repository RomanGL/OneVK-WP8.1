namespace OneVK.Enums.Common
{
    /// <summary>
    /// Перечисление ответов на запрос добавления друга.
    /// </summary>
    public enum VKAddFriend : byte
    {
        /// <summary>
        /// Заявка отправлена.
        /// </summary>
        Sent = 1,
        /// <summary>
        /// Заявка одобрена.
        /// </summary>
        Approved = 2,
        /// <summary>
        /// Заявка отправлена отправлена.
        /// </summary>
        Resent = 4
    }
}
