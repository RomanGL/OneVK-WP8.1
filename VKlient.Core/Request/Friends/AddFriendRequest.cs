using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добавление друга 
    /// или на подтверждение заявки.
    /// </summary>
    public class AddFriendRequest : BaseRequest, IVKRequestOld
    {
        private long _userID;
        
        /// <summary>
        /// Идентификатор пользователя, которому необходимо 
        /// отправить заявку, либо заявку от которого 
        /// необходимо одобрить.
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
        /// Текст сопроводительного сообщения для заявки на 
        /// добавление в друзья. 
        /// Максимальная длина сообщения — 500 символов.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.FriendsAdd; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["user_id"] = UserID.ToString();
            if (!String.IsNullOrWhiteSpace(Text))
                parameters["text"] = Text;

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным 
        /// идентификатором пользователя, которого требуется 
        /// добавить в друзья.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя, 
        /// которого требуется добавить в друзья.</param>
        public AddFriendRequest(long userID)
            : this(userID, null) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным
        /// идентификатором пользователя, которого требуется добавить, 
        /// и текстом сообщения для него.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя, 
        /// которого требуется добавить в друзья.</param>
        /// <param name="text">Текст сообщения, которое будет 
        /// отправлено пользователю вместе с заявкой.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public AddFriendRequest(long userID, string text)
        {
            UserID = userID;
            Text = text;
        }
    }
}
