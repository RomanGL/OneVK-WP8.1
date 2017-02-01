using OneVK.Enums.Common;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на отправку жалобы 
    /// на фотографию.
    /// </summary>
    public class ReportPhotoCommentRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Идентификатор владельца комментария.
        /// </summary>
        public long OwnerID { get; private set; }

        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public ulong CommentID { get; private set; }

        /// <summary>
        /// Причина жалобы.
        /// </summary>
        public VKReason Reason { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца комментария.</param>
        /// <param name="commentID">Идентификатор комментария.</param>
        public ReportPhotoCommentRequest(long ownerID, ulong commentID)
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
            if ((int)Reason != 0) parameters["reason"] = ((int)Reason).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoReportComment; }
    }
}
