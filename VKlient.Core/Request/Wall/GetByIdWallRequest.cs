using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Возвращает запись по ее ID
    /// </summary>
    public class GetByIdWallRequest : IVKRequestOld
    {
        private string _posts;
        private int _extended;
        private int _copyHistoryDepth;

        /// <summary>
        /// Возвращать объекты пользователей и групп
        /// </summary>
        public int Extended {
            get { return _extended; }
            set
            {
                if ((value < 0) || (value > 1))
                    throw new ArgumentOutOfRangeException(Extended.GetType().Name,
                        "Значение может равняться либо 0, либо 1.");
                _extended = value;
            }}

        /// <summary>
        /// Глубина репостов
        /// </summary>
        public int CopyHistoryDepth {
            get { return _copyHistoryDepth; }
            set
            {
                if (CopyHistoryDepth < 0)
                    throw new ArgumentOutOfRangeException(CopyHistoryDepth.GetType().Name,
                        "Значение должно быть положительным");
                _copyHistoryDepth = value;
            }
        }

        /// <summary>
        /// Список ID постов, разделенных запятой
        /// </summary>
        public string Posts {
            get { return _posts; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Необходимо ввести значение");
                _posts = value;
            }
        }

        /// <param name="posts">Список ID постов, разделенных запятой</param>
        public GetByIdWallRequest(string posts)
        {
            Posts = posts;

            // Значения по умолчанию
            Extended = 0;
            CopyHistoryDepth = 2;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>()
            {
                {"posts", Posts}
            };

            if (Extended != 0)
                parameters["extended"] = Extended.ToString();
            if (CopyHistoryDepth != 2)
                parameters["copy_history_depth"] = CopyHistoryDepth.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает метод, для которого предназначен запрос.
        /// </summary>
        public string GetMethod()
        {
            return VKMethodsConstants.WallGetByID;
        }
    }
}
