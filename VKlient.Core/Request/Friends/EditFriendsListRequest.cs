using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на изменение списка друзей.
    /// </summary>
    public class EditFriendsListRequest : AddFriendsListRequest
    {
        private int _listID;
        private List<long> _addUserIDs;
        private List<long> _deleteUserIDs;

        /// <summary>
        /// Название списка.
        /// </summary>
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        /// <summary>
        /// Идентификатор списка, который требуется изменить.
        /// </summary>
        public int ListID
        {
            get { return _listID; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("ListID",
                        "Идентификатор списка не может быть отрицательным.");
                _listID = value;
            }
        }

        /// <summary>
        /// Идентификаторы пользователей, которые 
        /// требуется добавить в список.
        /// </summary>
        public List<long> AddUserIDs
        {
            get { return _addUserIDs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("AddUserIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("AddUserIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("AddUserIDs",
                        "Идентификатор польователя должен быть положительным.");
                _addUserIDs = value;
            }
        }

        /// <summary>
        /// Идентификаторы пользователей, которые 
        /// требуется удалить из списка.
        /// </summary>
        public List<long> DeleteUserIDs
        {
            get { return _deleteUserIDs; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("DeleteUserIDs",
                        "Объект должен быть инициализирован.");
                else if (value.Count == 0)
                    throw new ArgumentException("DeleteUserIDs",
                        "Количество элементов должно быть больше нуля.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("DeleteUserIDs",
                        "Идентификатор польователя должен быть положительным.");
                _deleteUserIDs = value;
            }
        }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.FriendsEditList; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["list_id"] = ListID.ToString();
            if (UserIDs == null)
            {
                if (AddUserIDs != null)
                    parameters["add_user_ids"] = String.Join(",", AddUserIDs);
                if (DeleteUserIDs != null)
                    parameters["delete_user_ids"] = String.Join(",", DeleteUserIDs);
            }

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с 
        /// заданным идентификатором списка.
        /// </summary>
        /// <param name="listID">Идентификатор списка, 
        /// который требуется изменить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public EditFriendsListRequest(int listID)
        {
            ListID = listID;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с 
        /// заданным идентификатором списка и новым 
        /// списком пользователей в нем.
        /// </summary>
        /// <param name="listID">Идентификатор списка, 
        /// который требуется изменить.</param>
        /// <param name="userIDs">Список идентификаторов 
        /// пользователей, которые должны быть в нем.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        public EditFriendsListRequest(int listID, List<long> userIDs)
            : this(listID)
        {
            UserIDs = userIDs;
        }
    }
}
