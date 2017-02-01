using OneVK.Model.Video;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение результатов поиска по видеозаписям c расширенной информацией.
    /// </summary>
    public class VideoSearchExtendedRequest : VideoSearchBaseRequest<VKCountedItemsObject<VKVideoExtended>>
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
        /// Инициализирует новый экземпляр класса c заданной строкой поискового запроса.
        /// </summary>
        /// <param name="query">Строка поискового запроса.</param>
        /// <exception cref="ArgumentException"/>
        public VideoSearchExtendedRequest(string query)
            : base(query) { }
    }
}
