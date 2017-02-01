using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Helpers;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на создание пустого альбома для фотографий.
    /// </summary>
    public class CreateAlbumPhotosRequest : BaseRequest, IVKRequestOld
    {
        private string _title;
        private long _groupID;

        /// <summary>
        /// Название альбома.
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set
            {
                if (String.IsNullOrWhiteSpace(Title) || Title.Length < 2)
                    throw new ArgumentException("Title",
                        "Название альбома не должно быть короче двух символов.");
                _title = value;
            }
        }

        /// <summary>
        /// Идентификатор сообщества, в котором создается альбом.
        /// </summary>
        public long GroupID
        {
            get { return _groupID;}
            set
            {
                DataValidationHelper.CheckLessThanZero(GroupID);
                _groupID = value;
            }
        }

        /// <summary>
        /// Текст описания альбома.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Уровень доступа к альбому (только для альбома пользователя).
        /// </summary>
        public VKAlbumPrivacy Privacy { get; set; }

        /// <summary>
        /// Уровень доступа к комментированию альбома (только для альбома пользователя).
        /// </summary>
        public VKAlbumPrivacy CommentPrivacy { get; set; }

        /// <summary>
        /// Кто может загружать фотографии в альбом (только для альбома сообщества).
        /// </summary>
        public VKBoolean UploadByAdminsOnly { get; set; }

        /// <summary>
        /// Отключено ли комментирование альбома (только для альбома сообщества).
        /// </summary>
        public VKBoolean CommentsDisabled { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["title"] = Title;
            if (!String.IsNullOrWhiteSpace(Description)) parameters["description"] = Description;
            if (GroupID > 0)
            {
                parameters["group_id"] = GroupID.ToString();
                if (UploadByAdminsOnly != VKBoolean.False) parameters["upload_by_admins_only"] = "1";
                if (CommentsDisabled != VKBoolean.False) parameters["comments_disabled"] = "1";
            }
            else
            {
                if (Privacy != VKAlbumPrivacy.AllUsers) parameters["privacy"] = ((byte)Privacy).ToString();
                if (CommentPrivacy != VKAlbumPrivacy.AllUsers) parameters["comment_privacy"] = ((byte)CommentPrivacy).ToString();
            }

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary
        public string GetMethod() { return VKMethodsConstants.PhotoCreateAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для создания пустого альбома с 
        /// фотографиями в коллекции пользователя.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <exception cref="ArgumentException"></exception>
        public CreateAlbumPhotosRequest(string title)
        {
            Initialize(title, 0);
        }
        
        /// <summary>
        /// Инициализирует новый экземпляр класса для создания пустого альбома с 
        /// фотографиями в коллекции пользователя.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <param name="groupID">Идентификатор сообщества, в котором создается альбом.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CreateAlbumPhotosRequest(string title, long groupID)
        {
            Initialize(title, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <param name="groupID">Идентификатор сообщества, в котором создается альбом.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(string title, long groupID)
        {
            Title = title;
            if (groupID != 0)
                GroupID = groupID;
        }
    }
}
