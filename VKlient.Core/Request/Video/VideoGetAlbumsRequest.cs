using OneVK.Model.Video;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка альбомов видеозаписей пользователя или сообщества. 
    /// </summary>
    public class VideoGetAlbumsRequest : VideoGetAlbumsBaseRequest<VKCountedItemsObject<VKVideoAlbum>>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public VideoGetAlbumsRequest()
            : base() { }
    }
}
