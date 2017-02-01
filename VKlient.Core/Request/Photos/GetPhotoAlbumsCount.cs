using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение количества фотоальбомов
    /// пользователя или сообщества.
    /// </summary>
    public class GetPhotoAlbumsCount : BaseVKRequest<int>
    {
        private long _ownerID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, количество фотоалбомов
        /// которого необходимо вернуть.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("OwnerID",
                        "Илентификатор пользователя или сообщества не может быть равен нулю.");
                _ownerID = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="ownerID">Идентификатор пользователя или сообщества, 
        /// количество фотоалбомов которого необходимо вернуть.</param>
        public GetPhotoAlbumsCount(long ownerID)
        {
            OwnerID = ownerID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID > 0)
                parameters["user_id"] = OwnerID.ToString();
            else
                parameters["group_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.PhotoGetAlbumsCount; }
    }
}
