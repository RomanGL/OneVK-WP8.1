using System;
using System.Collections.Generic;
using OneVK.Enums.Newsfeed;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Возвращает скрытый элемент обратно в новостную ленту пользователя.
    /// </summary>
    public class UnignoreNewsfeedItemRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Идентификатор пользователя или сообщества, которому
        /// принадлежит запись, которую необходимо вернуть в ленту.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Тип элемента, который нужно вернуть в ленту пользователя.
        /// </summary>
        public VKNewsfeedItemType Type { get; set; }

        /// <summary>
        /// Идентификатор элемента, который необходимо вернуть в ленту.
        /// </summary>
        public long ItemID { get; set; }

        /// <summary>
        /// Создает новый экземпляр класса с указанными параметрами.
        /// </summary>
        /// <param name="ownerID">Идентификатор пользователя или сообщества, которому
        /// принадлежит запись, которую необходимо вернуть в ленту.</param>
        /// <param name="type">Тип элемента, который нужно вернуть в ленту пользователя.</param>
        /// <param name="itemID">Идентификатор элемента, который необходимо вернуть в ленту.</param>
        public UnignoreNewsfeedItemRequest(long ownerID, VKNewsfeedItemType type, long itemID)
        {
            OwnerID = ownerID;
            Type = type;
            ItemID = itemID;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["type"] = Type.ToString().ToLower();
            parameters["owner_id"] = OwnerID.ToString();
            parameters["item_id"] = ItemID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedUnignoreItem;
        }
    }
}
