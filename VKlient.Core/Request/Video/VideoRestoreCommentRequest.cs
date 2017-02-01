using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на восстановление удаленного комментария к видеозаписи.
    /// </summary>
    public class VideoRestoreCommentRequest :  BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _commentID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор удаленного комментария.
        /// </summary>
        public ulong CommentID
        {
            get { return _commentID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _commentID = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором удаленного комментария.
        /// </summary>
        /// <param name="videoID">Идентификатор удаленного комментария.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoRestoreCommentRequest(ulong commentID)
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
        public override string GetMethod() { return VKMethodsConstants.VideoRestoreComment; }
    }
}
