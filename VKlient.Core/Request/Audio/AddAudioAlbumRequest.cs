using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на создание альбома аудиозаписей.
    /// </summary>
    public class AddAudioAlbumRequest : BaseVKRequest<VKAudioAddAlbumObject>
    {
        private string _title;

        /// <summary>
        /// Идентификатор сообщества, в коллекцию которого необходимо добавить
        /// альбом с аудиозаписями.
        /// </summary>
        public ulong GroupID { get; set; }

        /// <summary>
        /// Название альбома.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Title",
                        "Название альбома должно содержать хотя бы один символ.");
                _title = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["title"] = Title;
            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioAddAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для создания пустого альбома с 
        /// аудиозаписями в коллекции пользователя.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AddAudioAlbumRequest(string title)
        {
            Initialize(title, null);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса для создания пустого альбома с
        /// аудиозаписями в коллекции сообщества.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <param name="groupID">Идентификатор сообщества, в коллекцию которого необходимо добавить
        /// альбом с аудиозаписями.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public AddAudioAlbumRequest(string title, ulong groupID)
        {
            Initialize(title, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="title">Название альбома.</param>
        /// <param name="groupID">Идентификатор сообщества, в коллекцию которого необходимо добавить
        /// альбом с аудиозаписями.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(string title, ulong? groupID)
        {
            Title = title;
            if (groupID.HasValue)
                GroupID = groupID.Value;
        }
    }
}
