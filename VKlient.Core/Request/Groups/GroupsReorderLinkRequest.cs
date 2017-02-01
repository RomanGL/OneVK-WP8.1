using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на изменение местоположения ссылки в списке.
    /// </summary>
    public class GroupsReorderLinkRequest : BaseGroupsLinkRequest
    {
        /// <summary>
        /// Идентификатор ссылки, после которой необходимо разместить перемещаемую ссылку.
        /// </summary>
        public ulong After { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (After != 0) parameters["after"] = After.ToString();
            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsReorderLink; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public GroupsReorderLinkRequest(ulong groupID, ulong linkID)
            : base(groupID, linkID) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором ссылки,
        /// после которой необходимо разместить перемещаемую ссылку.
        /// </summary>
        /// <param name="after">Идентификатор ссылки, после которой необходимо разместить перемещаемую ссылку.</param>
        public GroupsReorderLinkRequest(ulong groupID, ulong linkID, ulong after)
            : base(groupID, linkID)
        {
            After = after;
        }
    }
}
