using OneVK.Enums.Common;
using OneVK.Enums.Group;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на назначение или разжалование руководителя в сообществе или изменение уровня его полномочий.
    /// </summary>
    public class GroupsEditManagerRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _groupID, _userID;

        /// <summary>
        /// Идентификатор сообщества.
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
        /// Идентификатор пользователя, чьи полномочия в сообществе нужно изменить.
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
        /// Уровень полномочий.
        /// </summary>
        public VKGroupRole Role { get; set; }

        /// <summary>
        /// Отображать ли пользователя в блоке контактов сообщества.
        /// </summary>
        public VKBoolean IsContact { get; set; }

        /// <summary>
        /// Должность пользователя, отображаемая в блоке контактов.
        /// </summary>
        public string ContactPosition { get; set; }

        /// <summary>
        /// Телефон пользователя, отображаемый в блоке контактов.
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Email пользователя, отображаемый в блоке контактов.
        /// </summary>
        public string ContactEmail { get; set; }

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
        public override string GetMethod() { return VKMethodsConstants.GroupsEditManager; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификаторами сообщества и пользователя.
        /// </summary>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <param name="userID">Идентификатор пользователя.</param>
        public GroupsEditManagerRequest(ulong groupID, ulong userID)
        {
            GroupID = groupID;
            UserID = userID;
        }
    }
}
