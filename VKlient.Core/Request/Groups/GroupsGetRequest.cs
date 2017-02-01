using OneVK.Enums.Groups;
using OneVK.Response;
using System;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка сообществ указанного пользователя.
    /// </summary>
    public class GroupsGetRequest : BaseGroupsGetRequest<VKCountedItemsObject<long>>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <exception cref="ArgumentException"/>
        public GroupsGetRequest(VKGroupsFilter filter)
            : base() { }
    }
}
