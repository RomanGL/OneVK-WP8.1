using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на поучение количества аудиозаписей
    /// у пользователя или сообщества.
    /// </summary>
    public class GetAudiosCountRequest : BaseVKRequest<int>
    {
        private long _ownerID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, колчество аудиозаписей
        /// которого необходимо вернуть.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
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
        /// <param name="ownerID">Идентификатор пользователя или сообщества, колчество аудиозаписей
        /// которого необходимо вернуть.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public GetAudiosCountRequest(long ownerID)
        {
            OwnerID = ownerID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.AudioGetCount; }
    }
}
