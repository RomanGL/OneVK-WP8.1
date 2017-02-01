using OneVK.Model.Photo;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка фотографий пользователя
    /// или сообщества.
    /// </summary>
    public class GetPhotosRequest : BaseGetPhotosRequest<VKCountedItemsObject<VKPhoto>>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetPhotosRequest() : base() { }
    }
}
