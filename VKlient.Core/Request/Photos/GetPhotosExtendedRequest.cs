using OneVK.Model.Photo;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса для получения списка фотографий
    /// пользвоателя или сообщества с расширенной информацией.
    /// </summary>
    public class GetPhotosExtendedRequest : BaseGetPhotosRequest<VKCountedItemsObject<VKPhotoExtended>>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetPhotosExtendedRequest() : base() { }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["extended"] = "1";

            return parameters;
        }
    }
}
