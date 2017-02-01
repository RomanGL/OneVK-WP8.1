using OneVK.Model.Comment;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка комментариев к видеозаписи с расширенной информацией.
    /// </summary>
    public class VideoGetCommentsExtendedRequest : VideoGetCommentsBaseRequest<VKVideoExtendedItemsObject<VKComment>>
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
        /// Инициализирует новый экземпляр класса
        /// c заданным идентификатором видеозаписи.
        /// </summary>
        /// <param name="query">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoGetCommentsExtendedRequest(ulong videoID)
            : base(videoID) { }
    }
}
