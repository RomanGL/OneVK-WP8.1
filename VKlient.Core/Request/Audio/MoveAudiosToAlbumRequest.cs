using OneVK.Enums.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на перемещение 
    /// аудиозаписи в альбом.
    /// </summary>
    public class MoveAudiosToAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _groupID;
        private long _albumID;
        private List<long> _audioIDs;

        /// <summary>
        /// Идентификатор сообщества, которому принадлежат аудиозписи,
        /// которые необходимо переметить.
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
        /// Идентфиикатор альбома, в который необходимо перемемтить аудиозаписи.
        /// </summary>
        public long AlbumID
        {
            get { return _albumID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("AlbumID",
                        "Идентификатор альбома аудиозаписей должен быть положительным числом.");
                _albumID = value;
            }
        }

        /// <summary>
        /// Список идентификаторов аудиозаписей, которые необходимо
        /// переместить.
        /// </summary>
        public List<long> AudioIDs
        {
            get { return _audioIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("AudioIDs",
                        "Коллекция должна быть инициализирована и должна содержать хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentException("AudioIDs",
                        "Колличество элементов в коллекции должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("AudioIDs",
                        "Идентификатор аудиозаписи должен быть положительным числом.");
                _audioIDs = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["audio_ids"] = String.Join(",", AudioIDs);
            if (AlbumID != 0) parameters["album_id"] = AlbumID.ToString();
            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioMoveToAlbum; }

        /// <summary>
        /// Инициализирует новый экземпляр класса для перемещения аудиозаписи
        /// пользователя.
        /// </summary>
        /// <param name="audioIDs">Список идентификаторов аудиозаписей, которые необходимо
        /// переместить.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public MoveAudiosToAlbumRequest(List<long> audioIDs)
        {
            Initialize(audioIDs, null);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса для перемещения аудиозаписи
        /// cсообщества.
        /// </summary>
        /// <param name="audioIDs">Список идентификаторов аудиозаписей, которые необходимо
        /// переместить.</param>
        /// <param name="groupID">Идентификатор сообщества, которому принадлежат аудиозписи,
        /// которые необходимо переметить.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public MoveAudiosToAlbumRequest(List<long> audioIDs, long groupID)
        {
            Initialize(audioIDs, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="audioIDs">Список идентификаторов аудиозаписей, которые необходимо
        /// переместить.</param>
        /// <param name="groupID">Идентификатор сообщества, которому принадлежат аудиозписи,
        /// которые необходимо переметить.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(List<long> audioIDs, long? groupID)
        {
            AudioIDs = audioIDs;
            if (groupID != null)
                GroupID = groupID.Value;
        }
    }
}
