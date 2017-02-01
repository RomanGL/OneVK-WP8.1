namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление ссылки из сообщества.
    /// </summary>
    public class GroupsDeleteLinkRequest : BaseGroupsLinkRequest
    {
        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsDeleteLink; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public GroupsDeleteLinkRequest(ulong groupID, ulong linkID)
            : base(groupID, linkID) { }
    }
}
