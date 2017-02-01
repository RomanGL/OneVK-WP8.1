using OneVK.Model.Comment;

namespace OneVK.Request.Wall
{
    /// <summary>
    /// Представляет запрос на получение списка комментариев к записи на стене. 
    /// </summary>
    public class GetWallCommentsRequest : BaseGetWallCommentsRequest<VKComment>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetWallCommentsRequest(long ownerID, ulong postID) : base(ownerID, postID) { }
    }
}
