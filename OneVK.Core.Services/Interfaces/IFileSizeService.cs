using System;
using System.Threading.Tasks;
using OneVK.Core.Models;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис для работы с удаленными по сети файлами.
    /// </summary>
    public interface IHttpFileService
    {
        /// <summary>
        /// Возвращает размер файла, расположенного на HTTP-ресурсе.
        /// </summary>
        /// <param name="url">Ссылка на файл.</param>
        /// <exception cref="InvalidOperationException"/>
        Task<FileSize> GetHttpFileSize(string url);
    }
}
