using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса на пометку сообщений как прочитанных.
    /// </summary>
    public class MarkAsReadRequest : BaseRequest, IVKRequestOld
    {
        private List<long> _messageIDs;
        private long _startMessageID;

        /// <summary>
        /// Список идентификаторов сообщений, которые необходимо пометить.
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
        /// Идентификатор чата или пользователя, если это диалог.
        /// </summary>
        public long PeerID { get; set; }

        /// <summary>
        /// При передаче этого параметра будут помечены как прочитанные все сообщения начиная с данного.
        /// </summary>
        public long StartMessageID
        {
            get { return _startMessageID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("StartMessageID",
                        "Количество символов не может быть отрицательным числом.");
                _startMessageID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageMarkAsImportant; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["message_ids"] = String.Join(",", MessageIDs);
            if (PeerID != 0) parameters["peer_id"] = PeerID.ToString();
            if (StartMessageID != 0) parameters["start_message_id"] = StartMessageID.ToString();

            return parameters;
        }
    }
}
