using System;
using System.Collections.Generic;

namespace OneVK.Request.LFRequests
{
    /// <summary>
    /// Представляет класс запроса Last.fm для списковых ответов.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseItemsRequest<T> : BaseLFRequest<T>
    {
        private int _page = 1;
        private int _count = 50;
        
        /// <summary>
        /// Номер страницы.
        /// </summary>
        public int Page
        {
            get { return _page; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Page",
                        "Номер страницы должен быть положительным.");
                _page = value;
            }
        }

        /// <summary>
        /// Количество возвращаемых элементов.
        /// По умолчанию 50.
        /// </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Count",
                        "Количество элементов не может быть отрицательным.");
                _count = value;
            }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (Page > 1) parameters["page"] = Page.ToString();
            if (Count != 50) parameters["limit"] = Count.ToString();

            return parameters;
        }  
    }
}
