using OneVK.Model.Gifts;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка подарков пользователя.
    /// </summary>
    public class GetGiftsRequest : BaseVKCountedRequest<VKCountedItemsObject<VKGiftItem>>
    {
        /// <summary>
        /// Идентификатор пользователя, для которого необходимо получить список подарков.
        /// </summary>
        public ulong UserID { get; set; }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GiftsGet; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (UserID != 0) parameters["user_id"] = UserID.ToString();
            return parameters;
        }
    }
}
