using OneVK.Model.Common;
using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение 
    /// списков для друзей.
    /// </summary>
    public class GetFriendsListsRequest : BaseVKCountedRequest<VKCountedItemsObject<VKFriendsList>>
    {
        /// <summary>
        /// Идентификатор пользователя, списки которого
        /// требуется получить.
        /// </summary>
        public ulong UserID { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.FriendsGetLists; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (UserID > 0) parameters["user_id"] = UserID.ToString();
            parameters["return_system"] = "1";

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с 
        /// идентификатором текущего пользователя.
        /// </summary>
        public GetFriendsListsRequest()
            : this(0) { }

        /// <summary>
        /// Инициализирует новый экземпляр класса с 
        /// заданным идентификатором пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя, 
        /// для которого требуется получить списки друзей.</param>
        public GetFriendsListsRequest(ulong userID)
        {
            UserID = userID;
        }
    }
}
