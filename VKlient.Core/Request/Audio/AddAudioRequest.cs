using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на добавление заданной аудиозаписи в 
    /// коллекцию пользователя или сообщества.
    /// </summary>
    public class AddAudioRequest : BaseVKRequest<long>
    {
        private long _ownerID;
        private long _audioID;
        private long _groupID;

        /// <summary>
        /// Идентификатор аудиозаписи.
        /// </summary>
        public long AudioID
        {
            get { return _audioID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("AudioID",
                        "Идентификатор аудиозаписи должен быть положительным числом.");
                _audioID = value;
            }
        }

        /// <summary>
        /// Идентификатор владельца аудиозаписи.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Идентификатор владельца аудиозаписи не может быть равен нулю.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор сообщества, в коллекцию которого необходимо добавить аудиозапись.
        /// </summary>
        public long GroupID
        {
            get { return _groupID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("GroupID",
                        "Идентификатор сообщества должен быть положительным числом.");
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["audio_id"] = AudioID.ToString();
            parameters["owner_id"] = OwnerID.ToString();
            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает свзязанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioAdd; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с идентификатором аудиозаписи
        /// и ее владельца для добавления аудиозаписи в коллекцию пользователя.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца аудиозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public AddAudioRequest(long audioID, long ownerID)
        {
            Initialize(audioID, ownerID, null);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с идентификаторами аудиозаписи,
        /// ее владельца и сообщества, в коллекцию которого ее нужно добавить.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца аудиозаписи.</param>
        /// <param name="groupID">Идентификатор сообщества, в коллекцию 
        /// которого необходимо добавить аудиозапись.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public AddAudioRequest(long audioID, long ownerID, long groupID)
        {
            Initialize(audioID, ownerID, groupID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца аудиозаписи.</param>
        /// <param name="groupID">Идентификатор сообщества, в коллекцию 
        /// которого необходимо добавить аудиозапись.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(long audioID, long ownerID, long? groupID)
        {
            AudioID = audioID;
            OwnerID = ownerID;
            if (groupID != null)
                GroupID = groupID.Value;
        }
    }
}
