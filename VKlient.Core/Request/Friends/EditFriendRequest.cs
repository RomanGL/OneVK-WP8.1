using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на редактирование списков для друга.
    /// </summary>
    public class EditFriendRequest : BaseRequest, IVKRequestOld
    {
        private long _userID;
        private List<int> _listIDs;

        /// <summary>
        /// Идентификатор пользователя (из числа друзей), для которого 
        /// требуется отредактировать списки.
        /// </summary>
        public long UserID
        {
            get { return _userID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("UserID",
                        "Значение должно быть больше нуля.");
                _userID = value;
            }
        }

        /// <summary>
        /// Идентификаторы списков, в которые требуется добавить друга.
        /// </summary>
        public List<int> ListIDs
        {
            get { return _listIDs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("ListIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("ListIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("ListIDs",
                        "Идентификатор списка должен быть положительным.");
                _listIDs = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.FriendsEdit; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (ListIDs != null) parameters["list_ids"] = String.Join(",0", ListIDs);
            parameters["user_id"] = UserID.ToString();
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным
        /// идентификатором друга, которого требуется убрать 
        /// из всех списков.
        /// </summary>
        /// <param name="userID">Идентификатор друга.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public EditFriendRequest(long userID)
        {
            UserID = userID;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным 
        /// идентификатором друга, которого требуется добавить 
        /// в указанные списки.
        /// </summary>
        /// <param name="userID">Идентификатор друга.</param>
        /// <param name="listIDs">Список идентификаторов списков,
        /// в которые требуется добавить друга.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public EditFriendRequest(long userID, List<int> listIDs)
            : this (userID)
        {
            ListIDs = listIDs;
        }
    }
}
