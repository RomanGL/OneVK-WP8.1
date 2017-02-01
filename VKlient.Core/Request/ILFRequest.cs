using System;
using System.Threading;
using System.Threading.Tasks;

namespace OneVK.Request
{
    /// <summary>
    /// Интерфейс запроса к сервису Last.fm.
    /// </summary>
    public interface ILFRequest<T> : IRequest
    {
        /// <summary>
        /// Выполнить запрос и вернуть результат.
        /// </summary>
        /// <param name="ct">Токен отмены операции.</param>
        Task<T> ExecuteAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        string GetMethod();
    }
}
