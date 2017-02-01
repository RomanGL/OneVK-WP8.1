using System;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление списка друзей.
    /// </summary>
    public class DeleteFriendsListRequest : BaseRequest, IVKRequestOld
    {
        private int _listID;

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
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.FriendsDeleteList; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с 
        /// заданным идентификатором списка для удаления.
        /// </summary>
        /// <param name="listID">Идентификатор списка друзей, 
        /// который требуется удалить.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public DeleteFriendsListRequest(int listID)
        {
            ListID = listID;
        }
    }
}
