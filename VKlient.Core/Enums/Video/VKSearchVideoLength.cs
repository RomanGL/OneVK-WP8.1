namespace OneVK.Enums.Video
{
    /// <summary>
    /// Перечисление типов длительности при поиске видео ВКонтакте.
    /// Комбинируется с <see cref="VKSearchVideoType"/>.
    /// </summary>
    public enum VKSearchVideoLength : byte
    {
        /// <summary>
        /// Видео любой длины.
        /// </summary>
        All,
        /// <summary>
        /// Возвращать только короткие видеозаписи.
        /// </summary>
        shortv,
        /// <summary>
        /// Возвращать только длинные видеозаписи.
        /// </summary>
        longv,
        /// <summary>
        /// Неизвестно.
        /// </summary>
        Unknown
    }
}
