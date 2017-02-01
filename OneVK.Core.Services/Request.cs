using System.Threading;
using System.Collections.Generic;
using OneVK.Core.Models;

namespace OneVK.Core.Services
{
    /// <summary>
    /// Представляет универсальный запрос к веб-сервису.
    /// </summary>
    /// <typeparam name="T">Тип результирующих данных.</typeparam>
    public sealed class Request<T> : IRequest<T>
    {        
        private readonly string method;
        private readonly Dictionary<string, string> parameters;

        /// <summary>
        /// Возвращает или задает HTTP-метод запроса к веб-сервису.
        /// </summary>
        public HttpMethod HttpMethod { get; set; }

        /// <summary>
        /// Возвращает токен отмены операции.
        /// </summary>
        public CancellationToken Token { get; private set; }

        /// <summary>
        /// Возвращает метод ВКонтакте.
        /// </summary>
        public string GetMethod() { return method; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Request{T}"/>.
        /// </summary>
        /// <param name="method">Метод для выполнения.</param>
        /// <param name="parameters">Словарь параметров.</param>
        /// <param name="token">Токен для отмены операции.</param>
        public Request(string method, Dictionary<string, string> parameters = null, CancellationToken token = default(CancellationToken))
        {
            this.method = method;
            this.parameters = parameters;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public Dictionary<string, string> GetParameters()
        {
            if (parameters != null) return parameters;
            return new Dictionary<string, string>();
        }
    }
}
