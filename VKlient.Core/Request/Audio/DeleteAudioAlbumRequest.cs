using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на удлаение альбома аудиозаписей из коллекции
    /// пользователя или сообщества.
    /// </summary>
    public class DeleteAudioAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _groupID;
        private long _albumID;

        /// <summary>
        /// Идентификатор сообщества, из коллекции которого требуется удалить 
        /// альбом с аудиозаписями.
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
        /// Идентификатор альбома.
        /// </summary>
        public long AlbumID
        {
            get { return _albumID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("GroupID",
                        "Идентификатор альбома аудиозаписей должен быть положительным числом.");
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
        public override string GetMethod() { return VKMethodsConstants.AudioDeleteAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для удаления заданного
        /// альбома аудиозаписей из коллекции пользователя.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DeleteAudioAlbumRequest(long albumID)
        {
            Initialize(albumID, null);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса для удаления заданного
        /// альбома аудиозаписей из коллекции сообщества.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="groupID">Идентификатор сообщества, из коллекции которого требуется удалить 
        /// альбом с аудиозаписями.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DeleteAudioAlbumRequest(long albumID, long groupID)
        {
            Initialize(albumID, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <param name="groupID">Идентификатор сообщества, из коллекции которого требуется удалить 
        /// альбом с аудиозаписями.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(long albumID, long? groupID)
        {
            AlbumID = albumID;
            if (groupID != null)
                GroupID = groupID.Value;
        }
    }
}
