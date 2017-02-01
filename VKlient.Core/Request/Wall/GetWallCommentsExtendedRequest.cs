using OneVK.Model.Comment;
using System.Collections.Generic;

namespace OneVK.Request.Wall
{
    /// <summary>
    /// Представляет запрос на получение списка комментариев к записи на стене с расширенной информацией. 
    /// </summary>
    public class GetWallCommentsExtendedRequest : BaseGetWallCommentsRequest<VKComment>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetWallCommentsExtendedRequest(long ownerID, ulong postID) : base(ownerID, postID) { }

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
