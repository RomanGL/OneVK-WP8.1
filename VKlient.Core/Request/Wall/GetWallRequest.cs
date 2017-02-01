using OneVK.Model.Wall;
using OneVK.Response;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение записей со стены
    /// пользователя или сообщества.
    /// </summary>
    public class GetWallRequest : BaseGetWallRequest<VKCountedItemsObject<VKWallPost>>
    {
        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public GetWallRequest() : base() { }
    }
}
