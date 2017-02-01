using OneVK.Model.Photo;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на получение информации о фотографиях
    /// пользователя или сообщества по их идентификторам.
    /// </summary>
    public class GetPhotosByIDRequest : BaseGetPhotosByIDRequest<List<VKPhoto>>
    {
        /// <summary>
        /// Баозовый конструктор.
        /// </summary>
        /// <param name="photos">Словарь идентификатороф фотографий в формате OwnerID-PhotoID.</param>
        public GetPhotosByIDRequest(List<string> photos) : base(photos) { }
    }
}
