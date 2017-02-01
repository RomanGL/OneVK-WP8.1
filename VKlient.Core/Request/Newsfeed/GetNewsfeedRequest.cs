using System;
using System.Collections.Generic;
using OneVK.Enums.Newsfeed;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение новостной ленты.
    /// </summary>
    public class GetNewsfeedRequest : BaseNewsfeedFeedGetRequest<VKNewsfeedGetObject>
    {
        /// <summary>
        /// Типы объектов, изменения комментариев к которым нужно вернуть.
        /// </summary>
        public List<VKNewsfeedFilters> Filters { get; set; }

        /// <summary>
        /// Источники новостей, новости от которых необходимо получить.
        /// </summary>
        public List<string> Sources { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetNewsfeedRequest()
            : base()
        {
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (Filters != null && Filters.Count != 0) parameters["filters"] = String.Join(",", Filters);
            if (Sources != null && Sources.Count != 0) parameters["source_ids"] = String.Join(",", Sources);
            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedGet;
        }
    }
}
