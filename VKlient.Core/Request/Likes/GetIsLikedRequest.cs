using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение информации, поставил 
    /// ли пользователь отметку "Мне нравится" и репостнул ли он объект.
    /// </summary>
    public class GetIsLikedRequest : BaseLikeRequest<VKIsLikedObject>
    {
        private long _userID;

        /// <summary>
        /// Идентификатор пользователя, отметку от 
        /// которого требуется проверить.
        /// </summary>
        public long UserID
        {
            get { return _userID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Идентификатор пользователя не может быть отрицательным.");
                _userID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот объект.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.LikesIsLiked; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (UserID > 0) parameters["user_id"] = UserID.ToString();
            return parameters;
        }
    }
}
