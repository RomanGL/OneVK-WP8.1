using OneVK.Model.Newsfeed;
using OneVK.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получение пользовательских
    /// списков новостей.
    /// </summary>
    public class GetNewsfeedListsRequest : GetNewsfeedListsBaseRequest<VKCountedItemsObject<VKNewsfeedList>>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetNewsfeedListsRequest()
            : base() { }
    }
}
