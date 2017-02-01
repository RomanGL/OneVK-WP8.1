using OneVK.Model.Group;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка сообществ указанного пользователя с расширенной информацией.
    /// </summary>
    public class GroupsGetExtendedRequest : BaseGroupsGetRequest<VKCountedItemsObject<VKGroupExtended>>
    {
        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["extended"] = "1";
            return parameters;
        }
        
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public GroupsGetExtendedRequest()
            : base() { }
    }
}
