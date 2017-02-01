using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Model.Profile;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка друзей онлайн.
    /// </summary>
    public class GetOnlineFriendsRequest : BaseGetProfilesRequest<VKCountedItemsObject<VKProfileShort>>
    {
        /// <summary>
        /// Будет возвращено дополнительное поле, если пользователь
        /// в сети с мобильного устройства.
        /// </summary>
        public VKBoolean OnlineMobile { get; set; }

        /// <summary>
        /// Способ сортировки результатов.
        /// Поддерживается только значение Hints и Random.
        /// </summary>
        public override VKFriendsOrder Order
        {
            get { return base.Order; }
            set
            {
                if (value == VKFriendsOrder.Name || value == VKFriendsOrder.Mobile)
                    throw new ArgumentOutOfRangeException("Order",
                        "Поддерживается только значение Hints и Random.");
                base.Order = value;
            }
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.FriendsGetOnline;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["order"] = Order.ToString().ToLower();
            return parameters;
        }
    }
}
