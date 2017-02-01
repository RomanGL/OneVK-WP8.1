using System;
using System.Threading;
using System.Threading.Tasks;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Интерфейс запроса к ВКонтакте.
    /// </summary>
    /// <typeparam name="T">Тип результирующих данных.</typeparam>
    public interface IVKRequest<T> : IRequest
    {
        /// <summary>
        /// Выполнить запрос и вернуть результат.
        /// </summary>
        Task<VKResponse<T>> ExecuteAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        string GetMethod();
    }
}
