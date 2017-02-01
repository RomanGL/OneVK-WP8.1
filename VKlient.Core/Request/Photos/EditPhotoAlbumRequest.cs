using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на редактирование альбома с фотографиями.
    /// </summary>
    public class EditPhotoAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _albumID;


        /// <summary>
        /// Идентификатор альбома, который необходимо отредактировать.
        /// </summary>
        public ulong AlbumID
        {
            get { return _albumID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("AlbumID",
                        "Идентификатор альбома не может быть равен нулю.");
                _albumID = value;
            }
        }

        /// <summary>
        /// Новое название альбома.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Новое описание альбома.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит альбом.
        /// </summary>
        public long OwnerID { get; set; }

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
        /// Базовый конструктор.
        /// </summary>
        /// <param name="albumID"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public EditPhotoAlbumRequest(ulong albumID)
        {
            AlbumID = albumID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["album_id"] = AlbumID.ToString();
            if (!String.IsNullOrWhiteSpace(Title)) parameters["title"] = Title;
            if (!String.IsNullOrWhiteSpace(Description)) parameters["description"] = Description;
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (UploadByAdminsOnly != VKBoolean.False) parameters["upload_by_admins_only"] = "1";
            if (CommentsDisabled != VKBoolean.False) parameters["comments_disabled"] = "1";
            if (Privacy != VKAlbumPrivacy.AllUsers) parameters["privacy"] = ((byte)Privacy).ToString();
            if (CommentPrivacy != VKAlbumPrivacy.AllUsers) parameters["comment_privacy"] = ((byte)CommentPrivacy).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запрсом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoEditAlbum; }
    }
}
