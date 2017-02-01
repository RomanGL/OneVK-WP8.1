using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на изменение статуса набора текста пользователем в диалоге. 
    /// </summary>
    public class SetActivityRequest : BaseRequest, IVKRequestOld
    {
        private long _userID;

        /// <summary>
        /// Идентификатор пользователя.
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
        /// typing — пользователь начал набирать текст.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageSetActivity; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["user_id"] = UserID.ToString();
            if (!String.IsNullOrWhiteSpace(Type)) parameters["type"] = Type;

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным 
        /// идентификатором пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя</param>
        public SetActivityRequest(long userID)
            : this(userID, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным 
        /// идентификатором пользователя и параметром начала набора текста.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="type">Пользователь начал набирать текст.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentException"/>
        public SetActivityRequest(long userID, string type)
        {
            UserID = userID;
            Type = type;
        }
    }
}
