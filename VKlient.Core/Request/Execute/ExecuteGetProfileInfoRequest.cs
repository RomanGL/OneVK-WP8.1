using OneVK.Response.Execute;
using System.Collections.Generic;

namespace OneVK.Request.Execute
{
    /// <summary>
    /// Представлет запрос на получение информации о профиле пользователя.
    /// </summary>
    public class ExecuteGetProfileInfoRequest : VKExecuteRequest<ExecuteGetProfileInfoResponse>
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
        public ExecuteGetProfileInfoRequest(ulong userID)
            : base("getProfileInfo")
        { UserID = userID; }
    }
}
