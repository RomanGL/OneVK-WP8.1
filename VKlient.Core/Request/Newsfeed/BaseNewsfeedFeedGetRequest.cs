using System;
using System.Collections.Generic;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый запрос для работы с методами получения постов новостной ленты.
    /// </summary>
    public abstract class BaseNewsfeedFeedGetRequest<T> : ExtendedNewsfeedBaseGetRequest<T>
    {
        /// <summary>
        /// Максимальное количество фотографий, которые необходимо вернуть.
        /// </summary>
        public uint MaxPhotos { get; set; }

        /// <summary>
        /// Возвращать также новости из скрытых источников.
        /// </summary>
        public VKBoolean ReturnBanned { get; set; }

        protected BaseNewsfeedFeedGetRequest()
            : base()
        {
            MaxPhotos = 5;
            ReturnBanned = VKBoolean.False;
        }

        /// <summary>
        /// Возвращает словарь параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();
            if (MaxPhotos != 5) parameters["max_photos"] = MaxPhotos.ToString();
            parameters["return_banned"] = ((byte)ReturnBanned).ToString();
            return parameters;
        }
    }
}
