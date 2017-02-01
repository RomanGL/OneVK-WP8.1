using OneVK.Enums.Common;
using OneVK.Response;
using System;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление отметки "Мне нравится".
    /// </summary>
    public class DeleteLikeRequest : BaseLikeRequest<VKLikesResponse>
    {
        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public sealed override string GetMethod() { return VKMethodsConstants.LikesDelete; }
    }
}
