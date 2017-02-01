namespace OneVK.Enums.Video
{
    /// <summary>
    /// Перечисление типов видео ВКонтакте.
    /// Комбинируется с <see cref="VKSearchVideoLength"/>.
    /// </summary>
    public enum VKSearchVideoType : byte
    {
        /// <summary>
        /// Видео любого типа.
        /// </summary>
        All,
        /// <summary>
        /// Видео в формате MP4.
        /// </summary>
        mp4,
        /// <summary>
        /// YouTube-видео.
        /// </summary>
        youtube,
        /// <summary>
        /// Vimeo-видео.
        /// </summary>
        vimeo,
        /// <summary>
        /// Неизвестно.
        /// </summary>
        Unknown
    }
}
