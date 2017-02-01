using System;
using System.Net;
using System.Threading.Tasks;
using OneVK.Core.Models;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет сервис для работы с удаленными по сети файлами.
    /// </summary>
    public class HttpFileService : IHttpFileService
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="HttpFileService"/>.
        /// </summary>
        public HttpFileService() { }

        /// <summary>
        /// Возвращает размер файла, расположенного на HTTP-ресурсе.
        /// </summary>
        /// <param name="url">Ссылка на файл.</param>
        /// <exception cref="InvalidOperationException"/>
        public async Task<FileSize> GetHttpFileSize(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                request.Method = "HEAD";

                var response = await request.GetResponseAsync();
                return FileSize.FromBytes((ulong)response.ContentLength);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Не удалось получить данные о размере файла.", ex);
            }
        }
    }
}
