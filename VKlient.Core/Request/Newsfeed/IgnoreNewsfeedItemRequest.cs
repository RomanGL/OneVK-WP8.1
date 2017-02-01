using System;
using System.Collections.Generic;
using OneVK.Enums.Newsfeed;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на скрытие записи из новостной ленты
    /// текущего пользователя.
    /// </summary>
    public class IgnoreNewsfeedItemRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Идентификатор пользователя или сообщества, которому
        /// принадлежит запись, которую необходимо скрыть.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Тип элемента, который нужно скрыть из новостной ленты пользователя.
        /// </summary>
        public VKNewsfeedItemType Type { get; set; }

        /// <summary>
        /// Идентификатор элемента, который необходимо скрыть.
        /// </summary>
        public long ItemID { get; set; }

        /// <summary>
        /// Создает новый экземпляр класса с указанными параметрами.
        /// </summary>
        /// <param name="ownerID">Идентификатор пользователя или сообщества, которому
        /// принадлежит запись, которую необходимо скрыть.</param>
        /// <param name="type">Тип элемента, который нужно скрыть из новостной ленты пользователя.</param>
        /// <param name="itemID">Идентификатор элемента, который необходимо скрыть.</param>
        public IgnoreNewsfeedItemRequest(long ownerID, VKNewsfeedItemType type, long itemID)
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
            return VKMethodsConstants.NewsfeedIgnoreItem;
        }
    }
}
