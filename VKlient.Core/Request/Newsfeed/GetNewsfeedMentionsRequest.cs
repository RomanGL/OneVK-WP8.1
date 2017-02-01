using OneVK.Model.Newsfeed;
using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение записей с 
    /// упоминаниями указанного пользователя.
    /// </summary>
    public class GetNewsfeedMentionsRequest : BaseNewsfeedFeedGetRequest<VKCountedItemsObject<VKNewsfeedMention>>
    {
        /// <summary>
        /// Идентификатор пользователя или сообщества.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetNewsfeedMentionsRequest()
        {
            DefaultCount = 20;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedGetMentions;
        }
    }
}
