using OneVK.Model.Profile;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение информации о пользователях.
    /// </summary>
    public class GetUsersRequest : BaseGetUsersRequest<List<VKProfileBase>>
    {
        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["fields"] = VKMethodsConstants.BaseProfileFields;
            return parameters;
        }
    }
}
