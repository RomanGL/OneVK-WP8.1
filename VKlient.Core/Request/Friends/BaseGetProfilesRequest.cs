using System;
using System.Collections.Generic;
using OneVK.Enums.Profile;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка профилей.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseGetProfilesRequest<T> : BaseVKCountedRequest<T>
    {
        private VKFriendsOrder _order = VKFriendsOrder.Hints;

        /// <summary>
        /// Идентификатор списка друзей. Учитывается, только если
        /// указан идентификатор текущего пользователя.
        /// </summary>
        public uint ListID { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; set; }

        /// <summary>
        /// Способ сортировки результатов.
        /// </summary>
        public virtual VKFriendsOrder Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (ListID > 0) parameters["list_id"] = ListID.ToString();
            if (UserID > 0) parameters["user_id"] = UserID.ToString();

            return parameters;
        }
    }
}
