using OneVK.Enums.Common;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для выполнения действий со ссылками сообщества.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseGroupsLinkRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _groupID, _linkID;

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
        /// Идентификатор ссылки.
        /// </summary>
        public ulong LinkID
        {
            get { return _linkID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _linkID = value;
            }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["group_id"] = GroupID.ToString();
            parameters["link_id"] = LinkID.ToString();

            return parameters;
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="groupID">Идентификатор сообщества.</param>
        /// <param name="linkID">Идентификатор ссылки.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        protected BaseGroupsLinkRequest(ulong groupID, ulong linkID)
        {
            GroupID = groupID; 
            LinkID = linkID;
        }
    }
}
