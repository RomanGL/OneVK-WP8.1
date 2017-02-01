using OneVK.Enums.App;
using Newtonsoft.Json;

namespace OneVK.Core.Transfer
{
    /// <summary>
    /// Интерфейс объектов, поддерживающих загрузку.
    /// </summary>
    public interface IDownloadSupport
    {
        /// <summary>
        /// Тип содержимого файла.
        /// </summary>
        FileContentType ContentType { get; }
        /// <summary>
        /// Расширение файла.
        /// </summary>
        string Extension { get; }
        /// <summary>
        /// Путь к файлу для загрузки.
        /// </summary>
        string Source { get; }
        /// <summary>
        /// Имя файла, которое будет применено к результирующему файлу.
        /// </summary>
        string FileName { get; }
    }
}
