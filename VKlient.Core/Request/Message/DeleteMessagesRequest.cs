using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса на удаление сообщений.
    /// </summary>
    public class DeleteMessagesRequest : BaseRequest, IVKRequestOld
    {
        private List<long> _messageIDs;

        /// <summary>
        /// Список идентификаторов сообщений, разделённых через запятую.
        /// </summary>
        public List<long> MessageIDs
        {
            get { return _messageIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("MessageIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("MessageIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("MessageIDs",
                        "Идентификатор сообщения должен быть положительным.");
                _messageIDs = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageDelete; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["message_ids"] = String.Join(",", MessageIDs);
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса со списком идентификаторов сообщений, разделённых через запятую, которые необходимо удалить.
        /// </summary>
        /// <param name="messageIDs">Список идентификаторов сообщений, разделённых через запятую.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public DeleteMessagesRequest(List<long> messageIDs)
        {
            MessageIDs = messageIDs;
        }
    }
}
