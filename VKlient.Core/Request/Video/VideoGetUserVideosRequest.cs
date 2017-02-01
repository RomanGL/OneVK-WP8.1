using OneVK.Model.Video;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка видеозаписей, на которых отмечен пользователь.
    /// </summary>
    public class VideoGetUserVideosRequest : VideoGetUserVideosBaseRequest<VKCountedItemsObject<VKVideoExtended>>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public VideoGetUserVideosRequest()
            : base() { }
    }
}
