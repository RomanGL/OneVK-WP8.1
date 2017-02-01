using OneVK.Enums.Common;
using OneVK.Enums.Photo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneVK.Request
{
    /// <summary>
    /// Базовый класс запроса на получение фотографий пользователя или сообщесва.
    /// </summary>
    /// <typeparam name="T">Тип возвращаемого значения.</typeparam>
    public abstract class BaseGetPhotosRequest<T> : BaseVKCountedRequest<T>
    {
        private long _albumID;
        private List<long> _photos;
        private VKPhotoFeedType? _feedType = null;
        private long _feed;

        /// <summary>
        /// Идентификатор владельца фотографий.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор альбома, фотографии из которого
        /// необходимо вернуть.
        /// </summary>
        public long AlbumID
        {
            get { return _albumID; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("AlbumID",
                        "Идентификатор альбома должен быть положительным числом.");
                _albumID = value;
            }
        }

        /// <summary>
        /// Идентификаторы фотографий, которые необходимо вернуть.
        /// </summary>
        public List<long> Photos
        {
            get { return _photos; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Photos",
                        "Коллекция должна быть инициализирована.");
                else if (value.Count == 0)
                    throw new ArgumentException("Photos",
                        "Коллекция доджна содержать хотябы один элемент.");
                else if (!value.All(e => e > 0))
                    throw new ArgumentOutOfRangeException("Photos",
                        "Идентификатор фотографии должен быть положительным числом.");
                _photos = value;
            }
        }
        /// <summary>
        /// Возвращать фотографии в антихронологическом порядке
        /// (сначала новые).
        /// </summary>
        public VKBoolean Reverse { get; set; }

        /// <summary>
        /// Возвращать только фотогарфии, загруженные пользователем
        /// или только те, на которых он был отмечен.
        /// </summary>
        public VKPhotoFeedType? FeedType
        {
            get { return _feedType; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("FeedType",
                        "Объект должен быть инициализирован.");
                _feedType = value;
            }
        }

        /// <summary>
        /// Время в формате unixtime, указывающее на день, фотографии, опубликованные
        /// в который необходимо вернуть.
        /// </summary>
        public long Feed
        {
            get { return _feed; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Feed",
                        "Время unixtime должно быть положительным числом.");
                _feed = value;
            }
        }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        public BaseGetPhotosRequest()
        {
            MaxCount = 1000;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (AlbumID != 0) parameters["album_id"] = AlbumID.ToString();
            if (Photos != null && Photos.Count != 0) parameters["photo_ids"] = String.Join(",", Photos);
            if (Reverse == VKBoolean.True) parameters["rev"] = "1";
            if (FeedType.HasValue)
                switch (FeedType.Value)
                {
                    case VKPhotoFeedType.Photo:
                        parameters["feed_type"] = "photo";
                        break;
                    case VKPhotoFeedType.PhotoTag:
                        parameters["feed_type"] = "photo_tag";
                        break;
                }
            if (Feed != 0) parameters["feed"] = Feed.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        /// <returns></returns>
        public override string GetMethod() { return VKMethodsConstants.PhotoGet; }
    }
}
