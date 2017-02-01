using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет класс запроса на получение результатов поиска
    /// по новостной ленте пользователя с дополнительной информацией.
    /// </summary>
    public class SearchNewsfeedExtendedRequest : BaseSearchNewsfeedRequest<VKNewsfeedSearchExtendedObject>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public SearchNewsfeedExtendedRequest() : base() { }
    }
}
