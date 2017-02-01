namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Тип доступа к сообществу ВКонтакте.
    /// </summary>
    public enum VKGroupAccess : byte
    {
        /// <summary>
        /// Открытое сообщество.
        /// </summary>
        Public = 0,
        /// <summary>
        /// Закрытое сообщество.
        /// </summary>
        Limited,
        /// <summary>
        /// Частное сообщество.
        /// </summary>
        Private
    }
}
