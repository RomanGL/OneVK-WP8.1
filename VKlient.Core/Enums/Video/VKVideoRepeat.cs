namespace OneVK.Enums.Video
{
    /// <summary>
    /// Перечисление состояний зацикливания видеозаписи.
    /// </summary>
    public enum VKVideoRepeat : byte
    {
        /// <summary>
        /// Видеозапись не зациклена.
        /// </summary>
        False = 0,
        /// <summary>
        /// Видеозапись зациклена.
        /// </summary>
        True = 1,
        /// <summary>
        /// Неизвестно.
        /// </summary>
        Unknown
    }
}
