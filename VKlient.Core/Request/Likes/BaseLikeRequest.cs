using System;
using System.Collections.Generic;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет базовый класс запроса для 
    /// методов работы с отметками "Мне нравится".
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseLikeRequest<T> : BaseVKRequest<T>
    {
        /// <summary>
        /// Тип объекта для отметки.
        /// </summary>
        public VKLikesTargetType TargetType { get; set; }

        /// <summary>
        /// Идентификатор владельца объекта.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор объекта.
        /// </summary>
        public ulong ItemID { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["item_id"] = ItemID.ToString();
            parameters["type"] = TargetType.ToString();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }
    }
}
