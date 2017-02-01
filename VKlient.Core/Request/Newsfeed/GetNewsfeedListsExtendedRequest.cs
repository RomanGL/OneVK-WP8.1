using OneVK.Model.Newsfeed;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    public class GetNewsfeedListsExtendedRequest : GetNewsfeedListsBaseRequest<VKCountedItemsObject<VKNewsfeedListExtended>>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetNewsfeedListsExtendedRequest()
            : base() { }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["extended"] = "1";

            return parameters;
        }
    }
}
