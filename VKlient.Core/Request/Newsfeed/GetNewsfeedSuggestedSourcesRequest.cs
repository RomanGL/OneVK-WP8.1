using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на получение сообществ и пользователей, 
    /// на которых текущему пользователю рекомендуется подписаться.
    /// </summary>
    public class GetNewsfeedSuggestedSourcesRequest : BaseCountedRequest
    {
        private List<VKUserFields> _fields;

        /// <summary>
        /// Количество пользователей и сообществ, которое 
        /// необходимо вернуть.
        /// </summary>
        public override uint Count
        {
            get { return base.Count; }
            set
            {
                if (value > 1000)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество источников, которое необходимо вернуть не может быть больше 1000.");
                base.Count = value;
            }
        }

        /// <summary>
        /// Нужно ли перемешивать возвращаемый список.
        /// </summary>
        public VKBoolean ShuffleSources { get; set; }

        /// <summary>
        /// Дополнительные поля профилей, которые необходимо вернуть.
        /// </summary>
        public List<VKUserFields> Fields
        {
            get { return _fields; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Fields",
                        "Коллекция должна быть инициализирована и должна содержать хотябы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("Fields",
                        "Коллекция должна содержать хотя бы один элемент.");
                _fields = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (Count != 20) parameters["count"] = Count.ToString();
            if (ShuffleSources != VKBoolean.False) parameters["shuffle"] = "1";
            if (Fields != null && Fields.Count != 0) parameters["fields"] = String.Join(",", Fields);

            return parameters;
        }

        /// <summary>
        /// Возвращает связаный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.NewsfeedGetSuggestedSources; }
    }
}
