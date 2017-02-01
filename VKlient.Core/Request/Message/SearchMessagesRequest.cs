using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Возвращает список найденных личных сообщений текущего пользователя по введенной строке поиска.
    /// </summary>
    public class SearchMessagesRequest : BaseCountedRequest, IVKRequestOld
    {
        private string _q;
        private long _previewLength;

        /// <summary>
        /// Подстрока, по которой будет производиться поиск.
        /// </summary>
        public string Q
        {
            get { return _q; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Q",
                        "Значение должно быть больше нуля.");
                _q = value;
            }
        }

        /// <summary>
        /// Количество символов, по которому нужно обрезать сообщение.
        /// Укажите 0, если Вы не хотите обрезать сообщение (по умолчанию сообщения не обрезаются).
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
        /// Количество сообщений, которое необходимо получить.
        /// </summary>
        public override uint Count
        {
            get { return base.Count; }
            set
            {
                if (value > 100)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество сообщений должно быть не меньше нуля, но не больше 100.");
                base.Count = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageSearch; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>();

            parameters["q"] = Q;
            if (PreviewLength != 0) parameters["preview_length"] = PreviewLength.ToString();
            if (Count == 20) parameters.Remove("count");

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданной
        /// подстрокой, по которой будет производиться поиск.
        /// </summary>
        /// <param name="q">Подстрока, по которой будет производиться поиск.</param>
        /// <exception cref="ArgumentRangeException"/>
        public SearchMessagesRequest(string q)
        {
            Q = q;
        }
    }
}
