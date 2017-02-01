using OneVK.Helpers;
using OneVK.Model.Video;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение информации об альбоме с видео.
    /// </summary>
    public class VideoGetAlbumByIDRequest : BaseVKCountedRequest<VKVideoAlbumExtended>
    {
        private ulong _albumID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит альбом.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор альбома.
        /// </summary>
        public ulong AlbumID
        {
            get { return _albumID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _albumID = value;
            }
        }

        /// <summary>
        /// Инициализирует экземпляр класса с заданным идентификатором альбома.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoGetAlbumByIDRequest(ulong albumID)
        {
            AlbumID = albumID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["album_id"] = AlbumID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGetAlbumByID; }
    }
}
