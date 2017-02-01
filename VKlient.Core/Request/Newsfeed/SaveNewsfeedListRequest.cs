using System;
using System.Collections.Generic;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет собой запрос на сохранение или изменение пользовательского
    /// списка новостей.
    /// </summary>
    public class SaveNewsfeedListRequest : BaseVKRequest<int>
    {
        private List<long> _sourceIDs;

        /// <summary>
        /// Идентификатор пользовательского списка новостей, который необходимо изменить.
        /// </summary>
        public uint ListID { get; set; }

        /// <summary>
        /// Название пользовательского списка.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Идентификаторы пользователей и сообществ, которые включены в список.
        /// </summary>
        public List<long> SourceIDs
        {
            get { return _sourceIDs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("SourceIDs",
                        "Коллекция должна быть инициализирована и должна содержать хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("SourceIDs",
                        "Количество элементов в коллекции должно бытьбольше нуля.");
                _sourceIDs = value;
            }
        }

        /// <summary>
        /// Нужно ли возвращать копии записей в списке новостей.
        /// </summary>
        public VKBoolean NoRepost { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="title">Название пользовательского списка.</param>
        public SaveNewsfeedListRequest(string title)
        {
            Title = title;
            NoRepost = VKBoolean.False;
        }

        /// <summary>
        /// Возвращет коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["title"] = Title;
            if (ListID != 0) parameters["list_id"] = ListID.ToString();
            if (SourceIDs != null && SourceIDs.Count != 0) parameters["source_ids"] = String.Join(",", SourceIDs);
            if (NoRepost == VKBoolean.True) parameters["no_reposts"] = "1";

            return parameters;
        }

        /// <summary>
        /// Возвращет связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedSaveList;
        }
    }
}