using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для добавление/удаления пользователя в беседу/из беседы.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseChatUserRequest<T> : BaseVKRequest<T>
    {
        private uint _chatID;
        private ulong _userID;
        
        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        public uint ChatID
        {
            get { return _chatID; }
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("ChatID",
                        "Идентификатор чата должен быть положительным.");
                _chatID = value;
            }
        }

        /// <summary>
        /// Идентификатор пользователя, которого необходимо исключить из беседы или добавить в неё.
        /// </summary>
        public ulong UserID
        {
            get { return _userID; }
            set
            {
                if (value == 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Идентификатор пользователя должен быть положительным.");
                _userID = value;
            }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (ChatID != 0) parameters["chat_id"] = ChatID.ToString();
            if (UserID != 0) parameters["user_id"] = UserID.ToString();

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификаторами беседы и пользователя.
        /// </summary>
        /// <param name="chatID">Идентификатор беседы.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        protected BaseChatUserRequest(uint chatID, ulong userID)
        {
            ChatID = chatID;
            UserID = userID;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        protected BaseChatUserRequest() { }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public override abstract string GetMethod();
    }
}
