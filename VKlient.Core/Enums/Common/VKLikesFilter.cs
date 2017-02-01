namespace OneVK.Enums.Common
{
    /// <summary>
    /// Перечисление фильтров списка пользователей, 
    /// поставивших отметку "Мне нравится".
    /// </summary>
    public enum VKLikesFilter : byte
    {
        /// <summary>
        /// Все.
        /// </summary>
        all = 0,
        /// <summary>
        /// Только отметки "Мне нравится".
        /// </summary>
        likes,
        /// <summary>
        /// Только репосты.
        /// </summary>
        copies
    }
}
