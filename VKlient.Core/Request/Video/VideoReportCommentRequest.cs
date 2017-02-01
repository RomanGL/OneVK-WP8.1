using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на жалобу на комментарий к видеозаписи.
    /// </summary>
    public class VideoReportCommentRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _ownerID;
        private ulong _commentID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор комментария.
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
        /// Тип жалобы.
        /// </summary>
        public VKReason Reason { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="ownerID">Идентификатор пользователя или сообщества, которому принадлежит видеозапись.</param>
        /// <param name="commentID">Идентификатор комментария.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoReportCommentRequest(long ownerID, ulong commentID)
        {
            OwnerID = ownerID;
            CommentID = commentID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["owner_id"] = OwnerID.ToString();
            parameters["comment_id"] = CommentID.ToString();
            if (Reason != VKReason.Spam) parameters["reason"] = ((byte)Reason).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoReportComment; }
    }
}
