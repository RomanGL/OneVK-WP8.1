using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    public class GetNewsfeedBannedExtendedRequest : GetNewsfeedBannedBaseRequest<VKNewsfeedGetBannedExtendedObject>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        GetNewsfeedBannedExtendedRequest()
            : base() { }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["extended"] = "1";
            parameters["fields"] = VKMethodsConstants.BaseProfileFields;

            return parameters;
        }
    }
}
