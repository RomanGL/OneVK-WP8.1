using OneVK.Model.Profile;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет расширенный запрос на получение информации о пользователях.
    /// </summary>
    public class GetUsersExtendedRequest : BaseGetUsersRequest<List<VKProfileExtended>>
    {
        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["fields"] = VKMethodsConstants.ExtendedProfileFields;
            return parameters;
        }
    }
}
