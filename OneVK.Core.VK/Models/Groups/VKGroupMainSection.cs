namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Главная секция в сообществе.
    /// </summary>
    public enum VKGroupMainSection : byte
    {
        /// <summary>
        /// Не казано.
        /// </summary>
        None = 0,
        /// <summary>
        /// Фотографии.
        /// </summary>
        Photos = 1,
        /// <summary>
        /// Обсуждения.
        /// </summary>
        Topics = 2,
        /// <summary>
        /// Аудиозаписи.
        /// </summary>
        Audios = 3,
        /// <summary>
        /// Видеозаписи.
        /// </summary>
        Videos = 4,
        /// <summary>
        /// Товары.
        /// </summary>
        Market = 5
    }
}
