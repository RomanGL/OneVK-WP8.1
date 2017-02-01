using OneVK.Model.Profile;
using System.Collections.Generic;

namespace OneVK.Request.Execute
{
    /// <summary>
    /// Представлет запрос на получение списка друзей онлайн.
    /// </summary>
    public class ExecuteGetOnlineFriendsRequest : VKExecuteRequest<List<VKProfileShort>>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; set; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["user_id"] = UserID.ToString();
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        public ExecuteGetOnlineFriendsRequest(ulong userID)
            : base("getOnlineFriends")
        { UserID = userID; }
    }
}
