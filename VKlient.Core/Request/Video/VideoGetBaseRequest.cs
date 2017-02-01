using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения информации о видеозаписях.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class VideoGetBaseRequest<T> : BaseVKCountedRequest<T>
    {
        private Dictionary<long, ulong> _videos;

        /// <summary>
        /// Идентификатор владельца видеозаписей.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификаторы видеозаписей и их владельцев, перечисленные через знак подчеркивания.
        /// </summary>
        public Dictionary<long, ulong> Videos
        {
            get { return _videos; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Videos",
                        "Коллекция должна быть инициализирована и должна содержать хотя бы один элемент.");
                else if (value.Count == 0)
                    throw new ArgumentOutOfRangeException("Videos",
                        "Коллекция должна содержать хотя бы один элемент.");
                _videos = value;
            }
        }

        /// <summary>
        /// Идентификатор альбома.
        /// </summary>
        public ulong AlbumID { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        protected VideoGetBaseRequest()
        {
            DefaultCount = 100;
            MaxCount = 200;
        }

        /// <summary>
        /// Инициализирует новый экземепляр класса с заданными идентификаторами видеозаписей и их владельцев,
        /// перечисленными через знак подчеркивания.
        /// </summary>
        protected VideoGetBaseRequest(Dictionary<long, ulong> videos)
        {
            DefaultCount = 100;
            MaxCount = 200;
            Videos = videos;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (Videos != null && Videos.Count != 0) parameters["videos"] = String.Join(",",
                Videos.Select(kp => kp.Key.ToString() + "_" + kp.Value));
            if (AlbumID != 0) parameters["album_id"] = AlbumID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGet; }
    }
}
