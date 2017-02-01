using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на удаление аудиозаписи из коллекции
    /// пользователя или сообщества.
    /// </summary>
    public class DeleteAudioRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _audioID;
        private long _ownerID;

        /// <summary>
        /// Идентификатор аудиозаписи, которую нужно удалить из коллекции
        /// пользователя или сообщества.
        /// </summary>
        public long AudioID
        {
            get { return _audioID; }
            set
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
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Идентификатор пользователя или сообщества не может быть равен нулю.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи, которую нужно удалить из коллекции
        /// пользователя или сообщества.</param>
        /// <param name="ownerID">Идентификатор владельца аудиозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DeleteAudioRequest(long audioID, long ownerID)
        {
            AudioID = audioID;
            OwnerID = ownerID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["audio_id"] = AudioID.ToString();
            parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioDelete; }
    }
}
