using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneVK.Enums.Profile;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения информации о беседе и её пользователях.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseGetChatRequest<T> : BaseVKRequest<T>
    {
        private List<long> _chatIDs;
        private List<VKUserFields> _fields;

        public bool IsSingle { get; set; } = true;

        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        public long ChatID { get; set; }

        /// <summary>
        /// Список идентификаторов бесед.
        /// </summary>
        public List<long> ChatIDs
        {
            get { return _chatIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("ChatIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("ChatIDs",
                        "Количество элементов должно быть больше нуля.");
                _chatIDs = value;
            }
        }

        /// <summary>
        /// Падеж, в котором будут возвращаться имена пользователей.
        /// </summary>
        public VKUserNameCase NameCase { get; set; }

        /// <summary>
        /// Cписок дополнительных полей профилей, которые необходимо вернуть.
        /// </summary>
        public List<VKUserFields> Fields
        {
            get { return _fields; }
            set
            {
                if (value != null && value.Count() == 0)
                    throw new ArgumentOutOfRangeException("Fields",
                        "Коллекция полей должна содержать как минимум один элемент. " +
                        "Если вам требуется получить все данные, используйте null.");
                _fields = value;
            }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (IsSingle)
                parameters["chat_id"] = ChatID.ToString();
            else
                parameters["chat_ids"] = String.Join(",", ChatIDs);
            if (NameCase != VKUserNameCase.nom)
                parameters["name_case"] = NameCase.ToString();
            if (Fields == null) parameters["fields"] = VKMethodsConstants.ExtendedProfileFields;
            else
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
