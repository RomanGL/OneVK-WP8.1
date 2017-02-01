using System;
using System.Collections.Generic;
using OneVK.Enums.Newsfeed;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на на отписку текущего пользователя
    /// от комментариев к заданноиу объекту.
    /// </summary>
    public class UnsubscribeNewsfeedRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _ownerID;
        private long _itemID;

        /// <summary>
        /// Тип объекта, от комментариев к которому нужно отписаться.
        /// </summary>
        public VKNewsfeedCommentsFilters Type { get; set; }

        /// <summary>
        /// Идентификатор владельца объекта.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Идентификатор владельца объекта не может быть отрицательным числом.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        public long ItemID
        {
            get { return _itemID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("ItemID",
                        "Идентификатор объекта не может быть отрицательным числом.");
                _itemID = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="type">Тип объекта, от комментариев к которому нужно отписаться.</param>
        /// <param name="itemID">Идентификатор объекта.</param>
        public UnsubscribeNewsfeedRequest(VKNewsfeedCommentsFilters type, int itemID)
        {
            Type = type;
            ItemID = itemID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["type"] = Type.ToString();
            parameters["item_id"] = ItemID.ToString();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        /// <returns></returns>
        public override string GetMethod() { return VKMethodsConstants.NewsfeedUnsubscribe; }
    }
}
