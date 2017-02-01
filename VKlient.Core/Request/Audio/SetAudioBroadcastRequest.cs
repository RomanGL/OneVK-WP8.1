using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на трансляцию аудиозаписи в 
    /// статус пользователя или сообщества.
    /// </summary>
    public class SetAudioBroadcastRequest : BaseVKRequest<List<long>>
    {
        private long _ownerID;
        private long _audioID;
        private List<long> _targetIDs;

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
                        "Идентификатор владельца аудиозаписи не может быть равен нулю.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентфикатор аудиозаписи.
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
        /// Идентификаторы сообществ и пользователя, в статус
        /// которым будет транслироваться аудиозапись.
        /// </summary>
        public List<long> TargetIDs
        {
            get { return _targetIDs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("TargetIDs",
                        "Коллекция должна быть инициализирована и должна содержать хотя бы один элемент.");
                else if (value.Count < 1 || value.Count > 20)
                    throw new ArgumentException("TargetIDs",
                        "Количество элементов в коллекции должно быть больше нуля, но не больше 20.");
                else if (!value.All(e => e != 0))
                    throw new ArgumentOutOfRangeException("TargetIDs",
                        "Идентификатор пользователя или сообщества не может быть равен нулю.");
                _targetIDs = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0 && AudioID > 0) parameters["audio"] = OwnerID + "_" + AudioID;
            if (TargetIDs != null && TargetIDs.Count != 0) parameters["target_ids"] = String.Join(",", TargetIDs);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioSetBroadcast; }
    }
}
