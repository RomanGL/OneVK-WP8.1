using OneVK.Enums.Common;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление комментария к видеозаписи.
    /// </summary>
    public class VideoDeleteCommentRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _commentID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public long CommentID
        {
            get { return _commentID; }
            set
            {
                DataValidationHelper.CheckGreaterThanZero(value);
                _commentID = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором комментария.
        /// </summary>
        /// <param name="videoID">Идентификатор комментария.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoDeleteCommentRequest(long commentID)
        {
            CommentID = commentID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["comment_id"] = CommentID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoDeleteComment; }
    }
}
