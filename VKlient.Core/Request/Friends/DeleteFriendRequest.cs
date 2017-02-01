using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление пользователя из списка друзей.
    /// </summary>
    public class DeleteFriendRequest : BaseRequest, IVKRequestOld
    {
        private long _userID;

        /// <summary>
        /// Иднтификатор пользователя.
        /// </summary>
        public long UserID
        {
            get { return _userID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Значение должно быть больше нуля.");
                _userID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.FriendsDelete; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["user_id"] = UserID.ToString();
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класс с заданным 
        /// идентификатором пользователя, которого требуется 
        /// удалить из списка друзей.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя, 
        /// которого требуется удалить из списка друзей.</param>
        public DeleteFriendRequest(long userID)
        {
            UserID = userID;
        }
    }
}
