using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет объект запроса для получения информации о беседе.
    /// </summary>
    public class GetChatRequest : BaseGetChatRequest<VKGetChatObject>
    {
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.MessageGetChat; }
    }
}
