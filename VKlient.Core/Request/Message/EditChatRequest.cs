using OneVK.Enums.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на изменение названия беседы.
    /// </summary>
    public class EditChatRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private uint _chatID;
        private string _title;

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
                        "Значение является обязательным параметром.");
                _chatID = value;
            }
        }

        /// <summary>
        /// Новое название беседы.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Title",
                        "Значение является обязательным параметром.");
                _title = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageEditChat; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["chat_id"] = ChatID.ToString();
            parameters["title"] = Title;

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными
        /// идентификатором беседы и её новым названием.
        /// </summary>
        /// <param name="chatID">Идентификатор беседы.</param>
        /// <param name="title">Новое название беседы.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public EditChatRequest(uint chatID, string title)
        {
            ChatID = chatID;
            Title = title;
        }
    }
}
