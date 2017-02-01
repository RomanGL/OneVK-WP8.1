using OneVK.Enums.Groups;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка сообществ указанного пользователя.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseGroupsGetRequest<T> : BaseVKCountedRequest<T>
    {
        /// <summary>
        /// Идентификатор пользователя, информацию о сообществах которого требуется получить.
        /// </summary>
        public ulong UserID { get; set; }

        /// <summary>
        /// Список фильтров сообществ, которые необходимо вернуть, перечисленные через запятую.
        /// </summary>
        public VKGroupsFilter Filter { get; set; }

        /// <summary>
        /// Список дополнительных полей, которые необходимо вернуть.
        /// </summary>
        public List<string> Fields { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (UserID != 0) parameters["user_id"] = UserID.ToString();
            if (Filter != VKGroupsFilter.all) parameters["filter"] = Filter.ToString();
            parameters["fields"] = "members_count,activity";

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsGet; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        protected BaseGroupsGetRequest()
        {
            MaxCount = 1000;
        }
    }
}
