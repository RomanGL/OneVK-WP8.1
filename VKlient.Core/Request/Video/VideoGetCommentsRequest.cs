using OneVK.Model.Comment;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка комментариев к видеозаписи.
    /// </summary>
    public class VideoGetCommentsRequest : VideoGetCommentsBaseRequest<VKCountedItemsObject<VKComment>>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// c заданным идентификатором видеозаписи.
        /// </summary>
        /// <param name="query">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoGetCommentsRequest(ulong videoID)
            : base(videoID) { }
    }
}
