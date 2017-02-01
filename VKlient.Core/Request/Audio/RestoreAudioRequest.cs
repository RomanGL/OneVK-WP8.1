using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на восстановление ранее удаленной 
    /// аудиозаписи.
    /// </summary>
    public class RestoreAudioRequest : BaseVKRequest<VKAudioRestoreObject>
    {
        private long _audioID;
        private long _ownerID;

        /// <summary>
        /// Идентификатор аудиозаписи.
        /// </summary>
        public long AudioID
        {
            get { return _audioID; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("AudioID",
                        "Идентификатор аудиозаписи должен быть больше нуля.");
                _audioID = value;
            }
        }

        /// <summary>
        /// Идентификатор аудиозаписи.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Идентификатор владельца аудиозаписи не может быть равен нулю.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи.</param>
        public RestoreAudioRequest(long audioID)
        {
            AudioID = audioID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["audio_id"] = AudioID.ToString();
            if (OwnerID > 0) parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioRestore; }
    }
}
