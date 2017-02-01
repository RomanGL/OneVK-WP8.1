using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый класс запросов для методов работы с новостной лентой.
    /// Расширяет BaseCountedRequest.
    /// </summary>
    public abstract class ExtendedNewsfeedBaseGetRequest<T> : BaseNewsfeedGetRequest<T>
    {
        /// <summary>
        /// Идентификатор отступа для получения следующей страницы новостей.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        protected ExtendedNewsfeedBaseGetRequest()
        {
            DefaultCount = 50;
            MaxCount = 100;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(From)) parameters["start_from"] = From;
            parameters["fields"] = VKMethodsConstants.BaseProfileFields;

            return parameters;
        }
    }
}
