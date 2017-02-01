using OneVK.Enums.Common;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на исключение пользователя из группы.
    /// </summary>
    public class GroupsRemoveUserRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _groupID, _userID;

        /// <summary>
        /// Идентификатор группы, из которой необходимо исключить пользователя.
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
        /// Идентификатор пользователя, которого нужно исключить.
        /// </summary>
        public ulong UserID
        {
            get { return _userID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _userID = value;
            }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["group_id"] = GroupID.ToString();
            parameters["user_id"] = UserID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsRemoveUser; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификаторами сообщества и пользователя.
        /// </summary>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        public GroupsRemoveUserRequest(ulong groupID, ulong userID)
        {
            GroupID = groupID;
            UserID = userID;
        }
    }
}
