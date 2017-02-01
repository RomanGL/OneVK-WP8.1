using System;
using System.Collections.Generic;
using OneVK.Response;
using OneVK.Enums.Account;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение счетчиков текущего пользователя.
    /// </summary>
    public class GetCountersRequest : BaseVKRequest<VKAccountGetCountersResponse>
    {
        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AccountGetCounters; }

        /// <summary>
        /// Фильтр счетчиков. Null или пустой список для получения всех счетчиков.
        /// </summary>
        public List<VKCountersTypes> Filter { get; set; }

        /// <summary>
        /// Возвращает слооварь параметров.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (Filter != null && Filter.Count > 0)
                parameters["filter"] = string.Join(",", Filter).ToLower();
            return parameters;
        }
    }
}
