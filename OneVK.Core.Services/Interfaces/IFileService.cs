using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис для работы с файлами.
    /// </summary>
    public interface IFileService
    {
        Task<bool> WriteText(string text, StorageFile file);
        Task<StorageFile> GetFile(string fileName);
    }
}
