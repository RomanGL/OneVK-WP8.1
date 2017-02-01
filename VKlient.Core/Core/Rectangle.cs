namespace OneVK.Core
{
    /// <summary>
    /// Содержит информацию о длине и ширине прямоугольной области.
    /// </summary>
    public struct Rectangle
    {
        /// <summary>
        /// Ширина области.
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Высота области.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр структуры с заданной шириной и высотой
        /// прямоугольной области.
        /// </summary>
        /// <param name="width">Ширина.</param>
        /// <param name="height">Высота.</param>
        public Rectangle(double width, double height)
            : this()
        {
            Width = width;
            Height = height;
        }
    }
}
