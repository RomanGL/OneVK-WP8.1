namespace OneVK.Model
{
    /// <summary>
    /// Интерфейс для типов, которые могут являться владельцами объектов ВКонтакте.
    /// </summary>
    public interface IVKItemOwner
    {
        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        long ID { get; set; }

        /// <summary>
        /// Заголовок владельца.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Фотография владельца.
        /// </summary>
        string PhotoURL { get; }
    }
}
