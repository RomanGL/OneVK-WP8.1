using System.Threading;
using System.Threading.Tasks;
using OneVK.Helpers;

namespace OneVK.Request.LFRequests
{
    /// <summary>
    /// Представляет базовый класс запросов к сервису Last.fm.
    /// Это абстрактный класс.
    /// </summary>
    /// <typeparam name="T">Тип данных, возвращаемых запросом.</typeparam>
    public abstract class BaseLFRequest<T> : BaseRequest, ILFRequest<T>
    {
        /// <summary>
        /// Выполнить запрос и вернуть результат.
        /// </summary>
        /// <param name="ct">Токен отмены операции.</param>
        public async Task<T> ExecuteAsync(CancellationToken ct = default(CancellationToken))
        {
            return await LFHelper.GetResponse<T>(this, ct);
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public abstract string GetMethod();
    }
}
