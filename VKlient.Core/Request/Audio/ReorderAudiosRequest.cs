using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на изменения положения аудиозаписи
    /// в списке пользователя или сообщества.
    /// </summary>
    public class ReorderAudiosRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _audioID;
        private long _ownerID;
        private long _before;
        private long _after;

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
        /// Идентификатор аудиозаписи, после которой следует поместить текущую аудиозапись.
        /// </summary>
        public long Before
        {
            get { return _before; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Before",
                        "Идентификатор аудиозаписи должен быть положительным числом.");
                _before = value;
            }
        }

        /// <summary>
        /// Идентификатор аудиозаписи, перед которой следует поместить текущую аудиозапись.
        /// </summary>
        public long After
        {
            get { return _after; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Before",
                        "Идентификатор аудиозаписи должен быть положительным числом.");
                _after = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="audioID">Идентификатор аудиозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ReorderAudiosRequest(long audioID)
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
            if (Before > 0) parameters["before"] = Before.ToString();
            if (After > 0) parameters["after"] = After.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioReorder; }
    }
}
