using OneVK.Enums.Common;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение данных для подключения к LongPoll-серверу.
    /// </summary>
    public class GetLongPollServerRequest : BaseVKRequest<VKLongPollServerData>
    {
        /// <summary>
        /// Требуется ли использовать SSL.
        /// </summary>
        public VKBoolean UseSSL { get; set; }

        /// <summary>
        /// Возвращать поле pts, необходимое для работы метода messages.getLongPollHistory
        /// </summary>
        public VKBoolean NeedPts { get; set; }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageGetLongPollServer; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (UseSSL == VKBoolean.True) parameters["use_ssl"] = "1";
            if (NeedPts == VKBoolean.True) parameters["need_pts"] = "1";

            return parameters;
        }
    }
}
