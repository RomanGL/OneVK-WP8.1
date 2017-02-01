using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Response;
using OneVK.Model.Message;

namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса для получения сообщений по их id.
    /// </summary>
    public class GetByIDRequest : BaseVKRequest<VKCountedItemsObject<VKMessage>>
    {
        private int _previewLength;
        private List<ulong> _messageIDs;

        /// <summary>
        /// Количество символов, по которому нужно обрезать сообщение.
        /// </summary>
        public int PreviewLength
        {
            get { return _previewLength; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("PreviewLength",
                        "Количество символов не может быть отрицательным числом.");
                _previewLength = value;
            }
        }

        /// <summary>
        /// Идентификаторы сообщений.
        /// </summary>
        public List<ulong> MessageIDs
        {
            get { return _messageIDs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MessageIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("MessageIDs",
                        "Количество элементов должно быть больше нуля.");
                _messageIDs = value;
            }
        }
        
        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageGetByID; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>();

            if (PreviewLength != 0) parameters["preview_length"] = PreviewLength.ToString();
            parameters["message_ids"] = String.Join(",", MessageIDs);

            return parameters;
        }
    }
}
