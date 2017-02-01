using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет универсальный запрос к ВКонтакте.
    /// </summary>
    /// <typeparam name="T">Тип результирующих данных.</typeparam>
    public sealed class UniversalVKRequest<T> : BaseVKRequest<T>
    {
        private readonly string _method;
        private readonly Dictionary<string, string> _parameters;

        /// <summary>
        /// Возвращает метод ВКонтакте.
        /// </summary>
        public override string GetMethod() { return _method; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UniversalVKRequest{T}"/>.
        /// </summary>
        /// <param name="method">Метод ВКонтакте.</param>
        /// <param name="parameters">Словарь параметров.</param>
        public UniversalVKRequest(string method, Dictionary<string, string> parameters = null)
        {
            _method = method;
            _parameters = parameters;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            if (_parameters != null) return _parameters;
            return base.GetParameters();
        }
    }
}
