using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на создание пустого альбома видеозаписей.
    /// </summary>
    public class VideoAddAlbumRequest : BaseVKRequest<VKVideoAddAlbumObject>
    {
        private string _title;

        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        public ulong GroupID { get; set; }

        /// <summary>
        /// Название альбома.
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

            if (GroupID != 0) parameters["group_id"] = GroupID.ToString();
            parameters["title"] = Title;
            if (Privacy != VKAlbumPrivacy.AllUsers) parameters["privacy"] = ((byte)Privacy).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoAddAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для создания пустого альбома с 
        /// видеозаписями в коллекции пользователя.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <exception cref="ArgumentException"></exception>
        public VideoAddAlbumRequest(string title)
        {
            Initialize(title, null);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса для создания пустого альбома с
        /// видеозаписями в коллекции сообщества.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoAddAlbumRequest(string title, ulong groupID)
        {
            Initialize(title, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(string title, ulong? groupID)
        {
            Title = title;
            if (groupID.HasValue)
                GroupID = groupID.Value;
        }
    }
}
