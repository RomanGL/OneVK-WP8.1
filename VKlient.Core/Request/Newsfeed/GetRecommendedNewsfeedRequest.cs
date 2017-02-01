using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение рекомендованных 
    /// пользователю новостей.
    /// </summary>
    public class GetRecommendedNewsfeedRequest : BaseNewsfeedFeedGetRequest<VKNewsfeedGetObject>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetRecommendedNewsfeedRequest()
            : base()
        {
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod()
        {
            return VKMethodsConstants.NewsfeedGetRecommended;
        }
    }
}
