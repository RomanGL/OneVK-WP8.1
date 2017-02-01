using OneVK.Enums.Common;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на отправку жалобы 
    /// на фотографию.
    /// </summary>
    public class ReportPhotoRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Идентификатор владельца фотографии.
        /// </summary>
        public long OwnerID { get; private set; }

        /// <summary>
        /// Идентификатор фотографии.
        /// </summary>
        public ulong PhotoID { get; private set; }

        /// <summary>
        /// Причина жалобы.
        /// </summary>
        public VKReason Reason { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца фотографии.</param>
        /// <param name="photoID">Идентификатор фотографии.</param>
        public ReportPhotoRequest(long ownerID, ulong photoID)
        {
            OwnerID = ownerID;
            PhotoID = photoID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["owner_id"] = OwnerID.ToString();
            parameters["photo_id"] = PhotoID.ToString();
            if (Reason != VKReason.Spam) parameters["reason"] = ((int)Reason).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoReport; }
    }
}
