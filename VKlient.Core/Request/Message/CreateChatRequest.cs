using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос создания беседы с несколькими участниками.
    /// </summary>
    public class CreateChatRequest : BaseRequest, IVKRequestOld
    {
        private List<long> _userIDs;

        /// <summary>
        /// Идентификаторы пользователей, которых нужно включить в мультидиалог.
        /// </summary>
        public List<long> UserIDs
        {
            get { return _userIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("UserIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("UserIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("UserIDs",
                        "Идентификатор пользователя должен быть положительным.");
                _userIDs = value;
            }
        }

        /// <summary>
        /// Название беседы.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public string GetMethod()
        {
            return VKMethodsConstants.MessageCreateChat;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["user_ids"] = String.Join(",", UserIDs);
            if (!String.IsNullOrWhiteSpace(Title))
                parameters["title"] = Title;

            return parameters;
        }
    }
}
