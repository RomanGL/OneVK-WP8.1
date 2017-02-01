using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на пометку сообщений как избранных или на снятие данной отметки с них.
    /// </summary>
    public class MarkAsImportantRequest : BaseRequest, IVKRequestOld
    {
        private List<long> _messageIDs;
        
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
        /// 1, если сообщения необходимо пометить, как важные.
        /// 0, если необходимо снять пометку.
        /// </summary>
        public VKBoolean Important { get; set; }

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
            parameters["important"] = ((byte)Important).ToString();

            return parameters;
        }
    }
}
