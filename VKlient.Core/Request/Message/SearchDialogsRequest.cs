using System;
using System.Collections.Generic;
using System.Text;
using OneVK.Enums.Profile;

namespace OneVK.Request
{
    /// <summary>
    /// Возвращает список найденных диалогов текущего пользователя по введенной строке поиска.
    /// </summary>
    public class SearchDialogsRequest : BaseRequest, IVKRequestOld
    {
        private int _limit;
        
        /// <summary>
        /// Подстрока, по которой будет производиться поиск.
        /// </summary>
        public string Q { get; set; }

        public int Limit
        {
            get { return _limit; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Limit",
                        "Значение должно быть больше нуля.");
                _limit = value;
            }
        }

        /// <summary>
        /// Список дополнительных полей профилей, которые необходимо вернуть.
        /// </summary>
        public List<VKUserFields> Fields { get; set; }
        
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageSearchDialogs; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(Q)) parameters["q"] = Q.ToString();
            if (Limit != 20) parameters["limit"] = Limit.ToString();
            if (Fields != null)
            {
                var builder = new StringBuilder();
                for (int i = 0; i < Fields.Count; i++)
                    builder.Append(Fields[i] + ",");
                parameters["fields"] = builder.ToString();
            }

            return parameters;
        }
    }
}
