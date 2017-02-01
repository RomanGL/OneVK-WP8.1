namespace OneVK.Request
{
    /// <summary>
    /// Объект запроса на получение списка пользователей мультидиалога по его id. 
    /// </summary>
    public class GetChatUsersRequest : BaseRequest, IVKRequestOld
    {
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public string GetMethod() { return VKMethodsConstants.MessageGetChatUsers; }
    }
}
