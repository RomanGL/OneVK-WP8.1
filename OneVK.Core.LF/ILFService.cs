using System.Threading.Tasks;
using OneVK.Core.LF.Models;
using OneVK.Core.Services;

namespace OneVK.Core.LF
{
    /// <summary>
    /// Представляет сервис для работы с Last.fm.
    /// </summary>
    public interface ILFService
    {
        /// <summary>
        /// Выполняет указанный запрос к Last.fm и возвращает ответ.
        /// </summary>
        /// <typeparam name="T">Тип результирующих данных.</typeparam>
        /// <param name="request">Объект запроса к Last.fm.</param>
        Task<T> ExecuteRequestAsync<T>(IRequest<T> request) where T : LFResponse;
    }
}
