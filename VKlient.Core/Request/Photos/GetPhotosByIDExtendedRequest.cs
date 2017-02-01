using OneVK.Model.Photo;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на получение информации о фотографиях
    /// пользователя или сообщества по их идентификаторам с расширенной информацией.
    /// </summary>
    public class GetPhotosByIDExtendedRequest : BaseGetPhotosByIDRequest<List<VKPhotoExtended>>
    {
        /// <summary>
        /// Баозовый конструктор.
        /// </summary>
        /// <param name="photos">Словарь идентификатороф фотографий в формате OwnerID-PhotoID.</param>
        public GetPhotosByIDExtendedRequest(List<string> photos) : base(photos) { }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["extended"] = "1";

            return parameters;
        }
    }
}
