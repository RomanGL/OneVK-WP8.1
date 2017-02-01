namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Перечисление типов открытости сообщества.
    /// </summary>
    public enum VKGroupIsClosed : byte
    {
        /// <summary>
        /// Открытое.
        /// </summary>
        Open = 0,
        /// <summary>
        /// Закрытое.
        /// </summary>
        Closed = 1,
        /// <summary>
        /// Частное.
        /// </summary>
        Private = 2
    }
}
