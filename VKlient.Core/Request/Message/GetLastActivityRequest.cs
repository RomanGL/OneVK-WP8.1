using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на возвращение текущего статуса и даты последней активности указанного пользователя.
    /// </summary>
    public class GetLastActivityRequest : BaseRequest, IVKRequestOld
    {
        private long _userID;
        
        /// <summary>
        /// Идентификатор пользователя, информацию о последней активности которого требуется получить.
        /// </summary>
        public long UserID
        {
            get { return _userID; }
            private set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Значение не должно быть равно 0.");
                _userID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageGetLastActivity; }

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
        /// Инициализирует новый экземпляр класса с заданным 
        /// идентификатором пользователя, информацию о последней активности которого требуется получить.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя,
        /// информацию о последней активности которого требуется получить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public GetLastActivityRequest(long userID)
        {
            UserID = userID;
        }
    }
}
