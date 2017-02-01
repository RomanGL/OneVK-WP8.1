using OneVK.Model.Video;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка альбомов видеозаписей пользователя или сообщества с расширенной информацией. 
    /// </summary>
    public class VideoGetAlbumsExtendedRequest : VideoGetAlbumsBaseRequest<VKCountedItemsObject<VKVideoAlbumExtended>>
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
        /// Базовый конструктор.
        /// </summary>
        public VideoGetAlbumsExtendedRequest()
            : base() { }
    }
}
