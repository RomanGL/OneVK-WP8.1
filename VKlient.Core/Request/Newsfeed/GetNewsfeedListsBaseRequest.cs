using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой базовый класс запроса на получение пользовательских
    /// списков новостей.
    /// </summary>
    public abstract class GetNewsfeedListsBaseRequest<T> : BaseVKRequest<T>
    {
        private List<uint> _ids;

        /// <summary>
        /// Идентификаторы пользовательских списков новостей, которые необходимо вернуть.
        /// </summary>
        public List<uint> IDs
        {
            get { return _ids; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("IDs",
                        "Коллекция должна быть инициализирована и должна иметь хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("IDs",
                        "Количество элементов в коллекции должно быть больше нуля.");
                _ids = value;
            }
        }

        /// <summary>
        /// Возвращает коллеуцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (IDs != null && IDs.Count != 0) parameters["list_ids"] = String.Join(",", IDs);

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedGetLists;
        }
    }
}
