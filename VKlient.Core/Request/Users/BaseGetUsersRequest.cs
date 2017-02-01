using OneVK.Enums.Profile;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет базовый запрос на получение информации о пользователях.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseGetUsersRequest<T> : BaseVKRequest<T>
    {        
        /// <summary>
        /// Указывает падеж, в котором требуется вернуть имя пользователя.
        /// </summary>
        public VKUserNameCase NameCase { get; set; }

        /// <summary>
        /// Идентификатор пользователя, информацию о котором требуется вернуть.
        /// </summary>
        public ulong UserID { get; set; }

        /// <summary>
        /// Список идентификаторов пользователей, информацию о которых требуется получить.
        /// </summary>
        public List<ulong> UserIDs { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public sealed override string GetMethod() { return VKMethodsConstants.UsersGet; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (NameCase != VKUserNameCase.nom) parameters["name_case"] = NameCase.ToString();
            if (UserIDs != null && UserIDs.Count > 0) parameters["user_ids"] = String.Join(",", UserIDs);
            else if (UserID != 0) parameters["user_ids"] = UserID.ToString();

            return parameters;
        }
    }
}
