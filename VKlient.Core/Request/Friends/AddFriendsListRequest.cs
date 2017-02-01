using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добаление списка друзей.
    /// </summary>
    public class AddFriendsListRequest : BaseRequest, IVKRequestOld
    {
        private string _name;
        private List<long> _userIDs;

        /// <summary>
        /// Название списка.
        /// </summary>
        public string Name
        {
            get { return _name; }
            protected set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name",
                        "Имя списка не может быть пустым.");
                _name = value;
            }
        }

        /// <summary>
        /// Идентификаторы пользователей, которые 
        /// требуется добавить в список.
        /// </summary>
        public List<long> UserIDs
        {
            get { return _userIDs; }
            protected set
            {
                if (value == null)
                    throw new ArgumentNullException("UserIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("UserIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("UserIDs",
                        "Идентификатор пользователя должен быть положительным.");
                _userIDs = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public virtual string GetMethod() { return VKMethodsConstants.FriendsAddList; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["name"] = Name;
            if (UserIDs != null) 
                parameters["user_ids"] = String.Join(",", UserIDs);

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным названием списка.
        /// </summary>
        /// <param name="name">Название создаваемого списка.</param>
        /// <exception cref="ArgumentException"/>
        public AddFriendsListRequest(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным названием 
        /// списка и списком идентификаторов друзей, которые будут 
        /// в него добавлены.
        /// </summary>
        /// <param name="name">Название создаваемого списка.</param>
        /// <param name="userIDs">Список идентификаторов друзей, 
        /// которые будут добавлены в список.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public AddFriendsListRequest(string name, List<long> userIDs)
            : this (name)
        {
            UserIDs = userIDs;
        }

        protected AddFriendsListRequest() { }
    }
}
