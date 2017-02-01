using OneVK.Enums.Common;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на одобрение заявки в группу от пользователя.
    /// </summary>
    public class GroupsApproveRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _groupID, _userID;
        
        /// <summary>
        /// Идентификатор группы, заявку в которую необходимо одобрить.
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
        /// Идентификатор пользователя, заявку которого необходимо одобрить.
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
        public override string GetMethod() { return VKMethodsConstants.GroupsApproveRequest; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификаторами сообщества и пользователя.
        /// </summary>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        public GroupsApproveRequest(ulong groupID, ulong userID)
        {
            GroupID = groupID;
            UserID = userID;
        }
    }
}
