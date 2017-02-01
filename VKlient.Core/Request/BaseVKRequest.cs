using OneVK.Helpers;
using OneVK.Response;
using System.Threading;
using System.Threading.Tasks;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый класс запросов к ВКонтакте.
    /// </summary>
    /// <typeparam name="T">Тип результирующих данных.</typeparam>
    public abstract class BaseVKRequest<T> : BaseRequest, IVKRequest<T>
    {
        /// <summary>
        /// Возвращает метод, для которого предназначен этот запрос.
        /// </summary>
        public abstract string GetMethod();

        /// <summary>
        /// Выполнить запрос и вернуть результат.
        /// </summary>
        public async Task<VKResponse<T>> ExecuteAsync(CancellationToken ct = default(CancellationToken))
        {
            return await VKHelper.GetResponse<T>(this, ct);
        }
    }
}
