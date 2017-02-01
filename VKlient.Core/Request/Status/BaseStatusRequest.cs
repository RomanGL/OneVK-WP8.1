using System.Collections.Generic;

namespace OneVK.Request.Status
{
    /// <summary>
    /// Представлет базовый запрос к методам работы со статусом.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class BaseStatusRequest<T> : BaseVKRequest<T>
    {
        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        public ulong GroupID { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (GroupID > 0) parameters["group_id"] = GroupID.ToString();
            return parameters;
        }
    }
}
