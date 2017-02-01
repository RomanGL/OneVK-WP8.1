using OneVK.Helpers;
using OneVK.Model.Common;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добавление ссылок в сообщество.
    /// </summary>
    public class GroupsAddLinkRequest : BaseVKRequest<VKLink>
    {
        private ulong _groupID;
        private string _link;

        /// <summary>
        /// Идентификатор сообщества, в которое добавляется ссылка.
        /// </summary>
        public ulong GroupID
        {
            get { return _groupID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _groupID = value;
            }
        }

        /// <summary>
        /// Адрес ссылки.
        /// </summary>
        public string Link
        {
            get { return _link; }
            private set
            {
                DataValidationHelper.CheckNullOrWhiteSpace(value);
                _link = value;
            }
        }

        /// <summary>
        /// Текст ссылки.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["group_id"] = GroupID.ToString();
            parameters["link"] = Link;
            if (!string.IsNullOrWhiteSpace(Text)) parameters["text"] = Text;

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsAddLink; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификатором сообщества и адресом ссылки.
        /// </summary>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <param name="link">Адрес ссылки.</param>
        public GroupsAddLinkRequest(ulong groupID, string link)
        {
            GroupID = groupID;
            Link = link;
        }
    }
}
