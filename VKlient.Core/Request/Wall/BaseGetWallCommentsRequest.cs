using OneVK.Enums.Common;
using System.Collections.Generic;

namespace OneVK.Request.Wall
{
    /// <summary>
    /// Класс запроса для получения списка комментариев к записи на стене.
    /// Это абстрактный класс.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения.</typeparam>
    public abstract class BaseGetWallCommentsRequest<T> : BaseVKCountedRequest<T>
    {
        /// <summary>
        /// Идентификатор владельца страницы (пользователь или сообщество).
        /// </summary>
        public long OwnerID { get; private set; }

        /// <summary>
        /// Идентификатор записи на стене.
        /// </summary>
        public ulong PostID { get; private set; }

        /// <summary>
        /// Возвращать информацию о лайках.
        /// </summary>
        public VKBoolean NeedLikes { get; set; }

        /// <summary>
        /// Идентификатор комментария, начиная с которого нужно вернуть список.
        /// </summary>
        public ulong StartCommentID { get; set; }

        /// <summary>
        /// Порядок сортировки комментариев.
        /// </summary>
        public VKSortByDate Sort { get; set; }

        /// <summary>
        /// Количество символов, по которому нужно обрезать текст комментария.
        /// </summary>
        public uint PreviewLength { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными значениями идентификаторами владельца и записи.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца страницы (пользователь или сообщество).</param>
        /// <param name="postID">Идентификатор записи на стене.</param>
        public BaseGetWallCommentsRequest(long ownerID, ulong postID)
        {
            MaxCount = 100;
            OwnerID = ownerID;
            PostID = postID;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.WallGetComments; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["owner_id"] = OwnerID.ToString();
            parameters["post_id"] = PostID.ToString();
            if (NeedLikes != VKBoolean.False) parameters["need_likes"] = "1";
            if (StartCommentID != 0) parameters["start_comment_id"] = StartCommentID.ToString();
            if (Sort != VKSortByDate.asc) parameters["sort"] = VKSortByDate.desc.ToString();
            if (PreviewLength != 0) parameters["preview_length"] = PreviewLength.ToString();

            return parameters;
        }
    }
}
