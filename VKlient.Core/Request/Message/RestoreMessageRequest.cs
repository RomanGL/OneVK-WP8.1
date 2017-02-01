using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса для восстановления удаленного сообщения.
    /// </summary>
    public class RestoreMessageRequest : BaseRequest, IVKRequestOld
    {
        private long _messageID;

        /// <summary>
        /// Идентификатор сообщения, которое нужно восстановить.
        /// </summary>
        public long MessageID
        {
            get { return _messageID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("MessageID",
                        "Значение является обязательным параметром и должно быть больше нуля.");
                _messageID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageRestore; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["message_id"] = MessageID.ToString();
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным 
        /// идентификатором сообщения, которое нужно восстановить.
        /// </summary>
        /// <param name="messageID">Идентификатор сообщения, которое нужно восстановить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public RestoreMessageRequest(long messageID)
        {
            MessageID = messageID;
        }
    }
}
