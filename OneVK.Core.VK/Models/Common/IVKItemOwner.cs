namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет владельца объекта ВКонтакте.
    /// </summary>
    public interface IVKItemOwner
    {
        /// <summary>
        /// Идентификатор владельца.
        /// </summary>
        long ID { get; }
        /// <summary>
        /// Имя владельца.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Фотография размером 50.
        /// </summary>
        string Photo50 { get; }
        /// <summary>
        /// Фотография размером 100.
        /// </summary>
        string Photo100 { get; }
        /// <summary>
        /// Фотография размером 200.
        /// </summary>
        string Photo200 { get; }
    }
}
