using OneVK.Model.Video;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка видеозаписей, на которых отмечен пользователь, с расширенной информацией.
    /// </summary>
    public class VideoGetUserVideosExtendedRequest : VideoGetUserVideosBaseRequest<VKVideoExtendedItemsObject<VKVideoExtended>>
    {
        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters["extended"] = "1";
            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public VideoGetUserVideosExtendedRequest()
            : base() { }
    }
}
