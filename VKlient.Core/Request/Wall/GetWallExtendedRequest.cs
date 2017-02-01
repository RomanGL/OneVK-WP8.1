using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение записей со стены
    /// пользователя или сообщества с расширенной информацией.
    /// </summary>
    public class GetWallExtendedRequest : BaseGetWallRequest<GetWallExtendedObject>
    {
        /// <summary>
        /// Базовый конструткор.
        /// </summary>
        public GetWallExtendedRequest() : base() { }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["extended"] = "1";

            return parameters;
        }
    }
}
