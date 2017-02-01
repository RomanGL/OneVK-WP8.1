using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет расширенный запрос на получение списка лайкнувших.
    /// </summary>
    public class GetLikesListExtendedRequest : BaseGetLikesListRequest<VKGetLikesListExtendedResponse>
    {
        /// <summary>
        /// Возвращает словарь параметров.
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
