using OneVK.Enums.Common;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка альбомов видеозаписей пользователя или сообщества.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class VideoGetAlbumsBaseRequest<T> : BaseVKCountedRequest<T>
    {
        /// <summary>
        /// Идентификатор владельца альбомов.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Требуется ли вернуть системные альбомы.
        /// </summary>
        public VKBoolean NeedSystem { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        protected VideoGetAlbumsBaseRequest()
        {
            DefaultCount = 50;
            MaxCount = 100;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["need_system"] = "1";

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGetAlbums; }
    }
}
