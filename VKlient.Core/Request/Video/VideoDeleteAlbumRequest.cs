using OneVK.Enums.Common;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление альбома видеозаписей.
    /// </summary>
    public class VideoDeleteAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _albumID;

        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        public ulong GroupID { get; set; }

        /// <summary>
        /// Идентификатор альбома.
        /// </summary>
        public ulong AlbumID
        {
            get { return _albumID; }
            private set
            {
                DataValidationHelper.CheckGreaterThanZero(value);
                _albumID = value;
            }
        }

         /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["album_id"] = AlbumID.ToString();
            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoDeleteAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для удаления заданного
        /// альбома видеозаписей из коллекции пользователя.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoDeleteAlbumRequest(ulong albumID)
        {
            Initialize(albumID, 0);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса для удаления заданного
        /// альбома видеозаписей из коллекции сообщества.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoDeleteAlbumRequest(ulong albumID, ulong groupID)
        {
            Initialize(albumID, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(ulong albumID, ulong? groupID)
        {
            AlbumID = albumID;
            if (groupID.HasValue)
                GroupID = groupID.Value;
        }
    }
}
