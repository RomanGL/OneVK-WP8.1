using OneVK.Enums.Common;
using OneVK.Response;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добавление отметки 
    /// "Мне нравится" к объекту.
    /// </summary>
    public class AddLikeRequest : BaseLikeRequest<VKLikesResponse>
    {
        /// <summary>
        /// Ключ доступа в случае работы с приватными объектами.
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Возвращает метод, который представляет этот запрос.
        /// </summary>
        public sealed override string GetMethod() { return VKMethodsConstants.LikesAdd; }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(AccessKey))
                parameters["access_key"] = AccessKey;

            return parameters;
        }
    }
}
