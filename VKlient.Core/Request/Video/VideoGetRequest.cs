using OneVK.Model.Video;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка видеозаписей пользователя или сообщества.
    /// </summary>
    public class VideoGetRequest : VideoGetBaseRequest<VKCountedItemsObject<VKVideoBase>>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public VideoGetRequest()
            : base() { }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public VideoGetRequest(Dictionary<long, ulong> videos)
            : base(videos) { }
    }
}
