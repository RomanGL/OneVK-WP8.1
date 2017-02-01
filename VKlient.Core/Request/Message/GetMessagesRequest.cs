using System;
using System.Collections.Generic;
using OneVK.Enums.Message;

namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса на возвращение списка входящих либо исходящих личных сообщений текущего пользователя.
    /// </summary>
    public class GetMessagesRequest : BaseCountedRequest, IVKRequestOld
    {
        private long _previewLength;
        private long _lastMessageID;

        /// <summary>
        /// Сообщения, которые требуется вернуть (входящие или исходящие).
        /// </summary>
        public VKMessageType Out { get; set; }

        /// <summary>
        /// Количество сообщений, которое необходимо получить.
        /// </summary>
        public override uint Count
        {
            get { return base.Count; }
            set
            {
                if (value > 200)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество сообщений должно быть не меньше нуля, но не больше 200.");
                base.Count = value;
            }
        }

        /// <summary>
        /// Максимальное время, прошедшее с момента отправки сообщения до текущего момента в секундах.
        /// </summary>
        public long TimeOffset { get; set; }

        /// <summary>
        /// Фильтр возвращаемых сообщений.
        /// </summary>
        public VKMessageFilters Filters { get; set; }

        /// <summary>
        /// Количество символов, по которому нужно обрезать сообщение.
        /// </summary>
        public long PreviewLength
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
        /// Идентификатор сообщения, полученного перед тем, которое нужно вернуть последним.
        /// </summary>
        public long LastMessageID
        {
            get { return _lastMessageID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("LastMessageID",
                        "Количество символов не может быть отрицательным числом.");
                _lastMessageID = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageGet; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>();

            parameters["out"] = ((byte)Out).ToString();
            if (TimeOffset != 0) parameters["time_offset"] = TimeOffset.ToString();
            parameters["filters"] = ((byte)Filters).ToString();
            if (PreviewLength != 0) parameters["preview_length"] = PreviewLength.ToString();
            if (LastMessageID != 0) parameters["last_message_id"] = LastMessageID.ToString();
            if (Count == 20) parameters.Remove("count");

            return parameters;
        }
    }
}
