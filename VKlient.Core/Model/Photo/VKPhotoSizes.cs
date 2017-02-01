
namespace OneVK.Model.Photo
{
    /// <summary>
    /// Перечисление размеров фотографии.
    /// </summary>
    public enum VKPhotoSizes : byte
    {        
        /// <summary>
        /// Неизвестно.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Максимальный размер 75*75.
        /// </summary>
        Photo75,
        /// <summary>
        /// Максимальный размер 130*130.
        /// </summary>
        Photo130,
        /// <summary>
        /// Максимальный размер 604*604.
        /// </summary>
        Photo604,
        /// <summary>
        /// Максимальный размер 807*807.
        /// </summary>
        Photo807,
        /// <summary>
        /// Максимальный размер 1280*1024.
        /// </summary>
        Photo1280,
        /// <summary>
        /// Максимальный размер 2560*2048.
        /// </summary>
        Photo2560
    }
}
