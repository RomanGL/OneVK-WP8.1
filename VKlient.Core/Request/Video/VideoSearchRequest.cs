using OneVK.Model.Video;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение результатов поиска по видеозаписям.
    /// </summary>
    public class VideoSearchRequest : VideoSearchBaseRequest<VKCountedItemsObject<VKVideoBase>>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса c заданной строкой поискового запроса.
        /// </summary>
        /// <param name="query">Строка поискового запроса.</param>
        /// <exception cref="ArgumentException"/>
        public VideoSearchRequest(string query)
            : base(query) { }
    }
}
