using OneVK.Enums.Group;
using OneVK.Helpers;
using OneVK.Model.Group;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение информации о заданном сообществе или о нескольких сообществах. 
    /// </summary>
    public class GetGroupsByIDRequest : BaseVKRequest<List<VKGroupExtended>>
    {
        private List<string> _groupIDs;
        private List<VKGroupFields> _fields;
        private string _groupID;

        /// <summary>
        /// Идентификаторы или короткие имена сообществ.
        /// </summary>
        public List<string> GroupIDs
        {
            get { return _groupIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("GroupIDs",
                        "Коллекция должна быть инициализирована и содержать хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentException("GroupIDs",
                        "Количество элементов в коллекции должно быть больше нуля.");
                _groupIDs = value;
            }
        }

        /// <summary>
        /// Идентификатор или короткое имя сообщества.
        /// </summary>
        public string GroupID
        {
            get { return _groupID; }
            private set
            {
                DataValidationHelper.CheckNullOrWhiteSpace(value);
                _groupID = value;
            }
        }

        /// <summary>
        /// Список дополнительных полей, которые необходимо вернуть.
        /// </summary>
        public List<VKGroupFields> Fields
        {
            get { return _fields; }
            private set
            {
                if (value != null && value.Count == 0)
                    throw new ArgumentOutOfRangeException("Fields",
                        "Коллекция полей должна содержать как минимум один элемент. " +
                        "Если вам требуется получить все данные, используйте null.");
                _fields = value;
            }
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (GroupIDs != null && GroupIDs.Count > 0) parameters["groups_ids"] = string.Join(",", GroupIDs);
            if (!string.IsNullOrWhiteSpace(GroupID)) parameters["group_id"] = GroupID;
            if (Fields == null) parameters["fields"] = VKMethodsConstants.GroupInfoFields;
            else
            {
                var builder = new StringBuilder();
                for (int i = 0; i < Fields.Count; i++)
                    builder.Append(Fields[i] + ",");
                parameters["fields"] = builder.ToString();
            }

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.GroupsGetByID; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором сообщества.
        /// </summary>
        /// <param name="groupID">Идентификатор сообщества.</param>
        public GetGroupsByIDRequest(string groupID)
        {
            GroupID = groupID;
        }
    }
}
