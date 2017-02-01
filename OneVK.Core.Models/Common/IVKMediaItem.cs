namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет медиаэлемент ВКонтакте.
    /// </summary>
    public interface IVKMediaItem
    {
        /// <summary>
        /// Возвращает тип медиаэлемента.
        /// </summary>
        VKMediaType Type { get; }

        /// <summary>
        /// Возвращает идентификатор владельца медиаэлемента.
        /// </summary>
        long OwnerID { get; }

        /// <summary>
        /// Возвращает идентификатор медиаэлемента.
        /// </summary>
        long ID { get; }
    }
}
