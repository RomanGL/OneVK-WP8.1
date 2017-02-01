namespace OneVK.Core
{
    /// <summary>
    /// Представляет элемент, поддерживающие отображение в виде миниатюры.
    /// </summary>
    public interface IThumbnailSupport
    {
        /// <summary>
        /// Данные о размере миниатюры.
        /// </summary>
        ThumbnailSize ThumbnailSize { get; set; }
        /// <summary>
        /// Ширина исходного элемента.
        /// </summary>
        double Width { get; }
        /// <summary>
        /// Высота исходного элемента.
        /// </summary>
        double Height { get; }
        /// <summary>
        /// Возвращает источник изображения миниатюры.
        /// </summary>
        string ThumbnailSource { get; }

        /// <summary>
        /// Возвращает коэффициент соотношения ширины к высоте.
        /// </summary>
        double GetRatio();
    }
}
