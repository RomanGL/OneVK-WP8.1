using OneVK.Enums.Common;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения информации о том, является ли пользователь участником сообщества.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class GroupsIsMemberBaseRequest<T> : BaseVKRequest<VKOperationIsSuccess>
    {
        /// <summary>
        /// Идентификатор или короткое имя сообщества.
        /// </summary>
        public string GroupID { get; private set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; private set; }

        /// <summary>
        /// Идентификаторы пользователей.
        /// </summary>
        public List<ulong> UserIDs { get; private set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["group_id"] = GroupID;
            if (UserID != 0) parameters["user_id"] = UserID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsIsMember; }
    }
}
