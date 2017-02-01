using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request.Status
{
    /// <summary>
    /// Представляет запрос на получение статуса пользователя или сообщества.
    /// </summary>
    public class StatusGetRequest : BaseStatusRequest<VKGetStatusResponse>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.StatusGet; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (UserID > 0) parameters["user_id"] = UserID.ToString();
            return parameters;
        }
    }
}
