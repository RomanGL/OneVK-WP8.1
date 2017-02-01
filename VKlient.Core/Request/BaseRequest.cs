using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый класс запроса.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseRequest : IRequest
    {
        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public virtual Dictionary<string, string> GetParameters()
        {
            return new Dictionary<string, string>();
        }
    }
}
