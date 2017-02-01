namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет информацию о файле, расположенном на удаленном HTTP-ресурсе.
    /// </summary>
    public sealed class HttpFileInfo
    {
        /// <summary>
        /// Возвращает размер файла.
        /// </summary>
        public FileSize Size { get; set; }

        /// <summary>
        /// Возвращает тип содержимого файла.
        /// </summary>
        public string MIMEType { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="HttpFileInfo"/>.
        /// </summary>
        public HttpFileInfo() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="HttpFileInfo"/>с указанным размером и
        /// MIME-типом содержимого.
        /// </summary>
        /// <param name="size">Размер файла.</param>
        /// <param name="mimeType">MIME-тип содержимого.</param>
        public HttpFileInfo(FileSize size, string mimeType)
        {
            Size = size;
            MIMEType = mimeType;
        }
    }
}
