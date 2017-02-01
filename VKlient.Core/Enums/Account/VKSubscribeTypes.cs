namespace OneVK.Enums.Account
{
    /// <summary>
    /// Перечисление типов уведомлений, которые требуется присылать.
    /// </summary>
    public enum VKSubscribeTypes : byte
    {
        /// <summary>
        /// Уведомление о полученных сообщениях.
        /// </summary>
        Message,
        /// <summary>
        /// Уведомления о заявках в друзья.
        /// </summary>
        Friend,
        /// <summary>
        /// Уведомления о вызовах.
        /// </summary>
        Call,
        /// <summary>
        /// Уведомления об ответах.
        /// </summary>
        Reply,
        /// <summary>
        /// Уведомления об упоминаниях.
        /// </summary>
        Mention,
        /// <summary>
        /// Уведомления о приглашениях в группы.
        /// </summary>
        Group,
        /// <summary>
        /// Уведомления об ответках "Мне нравится".
        /// </summary>
        Like
    }
}
