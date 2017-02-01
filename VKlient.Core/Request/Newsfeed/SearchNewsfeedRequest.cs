using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на получение резульятатов поиска
    /// по новостной ленте пользователя.
    /// </summary>
    public class SearchNewsfeedRequest : BaseSearchNewsfeedRequest<VKNewsfeedSearchObject>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public SearchNewsfeedRequest() : base() { }

        /// <summary>
        /// Возваращает коллекцию параметров.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters =  base.GetParameters();

            parameters["extended"] = "1";

            return parameters;
        }
    }
}
