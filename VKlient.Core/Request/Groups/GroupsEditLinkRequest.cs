using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на редактирование ссылки в сообществе.
    /// </summary>
    public class GroupsEditLinkRequest : BaseGroupsLinkRequest
    {
        private string _text;

        /// <summary>
        /// Новое описание ссылки.
        /// </summary>
        public string Text
        {
            get { return _text; }
            private set
            {
                DataValidationHelper.CheckNullOrWhiteSpace(value);
                _text = value;
            }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["text"] = Text.ToString();
            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsEditLink; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным новым описанием ссылки.
        /// </summary>
        /// <param name="text">Новое описание ссылки.</param>
        /// <exception cref="ArgumentException"/>
        public GroupsEditLinkRequest(ulong groupID, ulong linkID, string text)
            : base(groupID, linkID)
        {
            Text = text;
        }
    }
}
