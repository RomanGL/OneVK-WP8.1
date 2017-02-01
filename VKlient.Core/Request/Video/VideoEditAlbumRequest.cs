using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Helpers;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на редактирование названия альбома видеозаписей.
    /// </summary>
    public class VideoEditAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _groupID, _albumID;
        private string _title;

        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        public long GroupID
        {
            get { return _groupID; }
            set
            {
                DataValidationHelper.CheckLessThanZero(value);
                _groupID = value;
            }
        }

        /// <summary>
        /// Идентификатор альбома.
        /// </summary>
        public long AlbumID
        {
            get { return _albumID; }
            private set
            {
                DataValidationHelper.CheckGreaterThanZero(value);
                _albumID = value;
            }
        }

        /// <summary>
        /// Новое название для альбома.
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set
            {
                DataValidationHelper.CheckNullOrWhiteSpace(value);
                _title = value;
            }
        }

        /// <summary>
        /// Новый уровень доступа к альбому.
        /// </summary>
        public VKAlbumPrivacy Privacy { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();
            parameters["album_id"] = AlbumID.ToString();
            parameters["title"] = Title;
            if (Privacy != VKAlbumPrivacy.AllUsers) parameters["privacy"] = ((byte)Privacy).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoEditAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для редактирования названия альбома 
        /// видеозаписей пользователя.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="title">Новое название для альбома.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoEditAlbumRequest(long albumID, string title)
        {
            Initialize(albumID, title, 0);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса для редактирования названия альбома
        /// видеозаписей сообщества.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="title">Новое название для альбома.</param>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoEditAlbumRequest(long albumID, string title, long groupID)
        {
            Initialize(albumID, title, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="title">Новое название для альбома.</param>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(long albumID, string title, long? groupID)
        {
            AlbumID = albumID;
            Title = title;
            if (groupID.HasValue)
                GroupID = groupID.Value;
        }
    }
}
