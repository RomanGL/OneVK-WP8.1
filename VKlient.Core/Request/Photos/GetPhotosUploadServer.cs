using OneVK.Model.Photo;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет зпрос на получение адреса сервера ВКонаткте для
    /// отправки фотографии.
    /// </summary>
    public class GetPhotosUploadServer : BaseVKRequest<VKPhotoUploadServer>
    {
        private ulong _albumID;
        private ulong _groupID;

        /// <summary>
        /// Идентификатор альбома, в который необходимо загрузить фотографию.
        /// </summary>
        public ulong AlbumID { get; set; }

        /// <summary>
        /// Идентификатор сообщества, которому принадлежит альбом.
        /// </summary>
        public ulong GroupID { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (AlbumID != 0) parameters["album_id"] = AlbumID.ToString();
            if (GroupID != 0) parameters["group_id"] = GroupID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetUploadServer; }
    }
}
