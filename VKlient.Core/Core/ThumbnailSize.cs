namespace OneVK.Core
{
    /// <summary>
    /// Представляет данные для визуализации миниатюр.
    /// </summary>
    public struct ThumbnailSize
    {
        /// <summary>
        /// Ширина изображения в представлении.
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Высота изображения в представлении.
        /// </summary>
        public double Height { get; set; }
        public bool LastColumn { get; set; }
        public bool LastRow { get; set; }
    }
}
