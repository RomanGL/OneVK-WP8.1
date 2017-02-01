using OneVK.Model.Common;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение адреса сервера для отправки
    /// главной фотографии пользователя или сообщества.
    /// </summary>
    public class GetPhotoOwnerUploadServer : BaseVKRequest<VKUploadServerBase>
    {
        /// <summary>
        /// Идентификатор пользователя или сообщества, на страницу которого
        /// необходимо загрузить фотографию.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetOwnerUploadServer; }
    }
}
