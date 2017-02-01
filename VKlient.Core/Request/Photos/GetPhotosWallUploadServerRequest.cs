using OneVK.Model.Photo;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение информации о сервере для
    /// последующей загрузки на него фотографии для публикации на
    /// стене пользователя или сообщества.
    /// </summary>
    public class GetPhotosWallUploadServerRequest : BaseVKRequest<VKPhotoUploadServer>
    {
        /// <summary>
        /// Идентификатор сообщества, на стену 
        /// которого будет загружена фотография.
        /// </summary>
        public ulong GroupID { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (GroupID != 0) parameters["group_id"] = GroupID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetWallUploadServer; }

    }
}
