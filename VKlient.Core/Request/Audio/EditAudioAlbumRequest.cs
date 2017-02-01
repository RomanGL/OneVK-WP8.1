using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на изменение названия альбома
    /// с аудиозаписями пользователя или сообщества.
    /// </summary>
    public class EditAudioAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _albumID;
        private long _groupID;
        private string _title;

        /// <summary>
        /// Идентификатор альбома аудиозаписей.
        /// </summary>
        public long AlbumID
        {
            get { return _albumID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("AlbumID",
                        "Идентификатор альбома аудиозаписей должен быть положительным числом.");
                _albumID = value;
            }
        }

        /// <summary>
        /// Новое наименование альбома аудиозаписей.
        /// </summary>
        public string Title
        {
            get { return _title; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Title",
                        "Новое название альбома аудиозаписей должно содержать хотя бы один символ.");
                _title = value;
            }
        }

        /// <summary>
        /// Идентификатор сообщества, которому принадлежит альбом с аудиозаписями.
        /// </summary>
        public long GroupID
        {
            get { return _groupID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("GroupID",
                        "Идентификатор сообщества должен быть положительным числом.");
                _groupID = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["title"] = Title;
            parameters["album_id"] = AlbumID.ToString();
            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioEditAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для редактирования названия альбома 
        /// аудиозаписей пользователя.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома аудиозаписей.</param>
        /// <param name="title">Новое наименование альбома аудиозаписей.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public EditAudioAlbumRequest(long albumID, string title)
        {
            Initialize(albumID, title, null);
        }
        /// <summary>
        /// Инициализирует новый экземпляр класса для редактирования названия альбома
        /// аудиозаписей сообщества.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома аудиозаписей.</param>
        /// <param name="title">Новое наименование альбома аудиозаписей.</param>
        /// <param name="groupID">Идентификатор сообщества, которому принадлежит 
        /// альбом с аудиозаписями.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public EditAudioAlbumRequest(long albumID, string title, long groupID)
        {
            Initialize(albumID, title, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома аудиозаписей.</param>
        /// <param name="title">Новое наименование альбома аудиозаписей.</param>
        /// <param name="groupID">Идентификатор сообщества, которому принадлежит 
        /// альбом с аудиозаписями.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(long albumID, string title, long? groupID)
        {
            AlbumID = albumID;
            Title = title;
            if (groupID != null)
                GroupID = groupID.Value;
        }
    }
}
