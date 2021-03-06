﻿using OneVK.Model.Video;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка видеозаписей пользователя или сообщества с расширенной информацией.
    /// </summary>
    public class VideoGetExtendedRequest : VideoGetBaseRequest<VKCountedItemsObject<VKVideoExtended>>
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
        public VideoGetExtendedRequest()
            : base() { }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public VideoGetExtendedRequest(Dictionary<long, ulong> videos)
            : base(videos) { }
    }
}
