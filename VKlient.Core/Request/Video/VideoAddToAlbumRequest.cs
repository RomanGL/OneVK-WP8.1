using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добавление видеозаписи в альбом.
    /// </summary>
    public class VideoAddToAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _ownerID;
        private ulong _albumID, _videoID;
        private List<ulong> _albumIDs;

        public bool IsSingle { get; private set; }

        /// <summary>
        /// Идентификатор владельца альбома, в который нужно добавить видео.
        /// </summary>
        public long TargetID { get; set; }

        /// <summary>
        /// Идентификатор альбома, в который нужно добавить видео.
        /// </summary>
        public ulong AlbumID
        {
            get { return _albumID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _albumID = value;
            }
        }

        /// <summary>
        /// Идентификаторы альбомов, в которые нужно добавить видео.
        /// </summary>
        public List<ulong> AlbumIDs
        {
            get { return _albumIDs; }
            private set
            {
                if (value == null)
                    throw new ArgumentNullException("AlbumIDs",
                        "Объект должен быть инициализирован.");
                _albumIDs = value;
            }
        }

        /// <summary>
        /// Идентификатор владельца видеозаписи.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _ownerID = value;
            }
        }

        /// <summary>
        /// Идентификатор видеозаписи.
        /// </summary>
        public ulong VideoID
        {
            get { return _videoID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _videoID = value;
            }
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (TargetID != 0) parameters["target_id"] = TargetID.ToString();
            if (IsSingle) parameters["album_id"] = AlbumID.ToString();
            else parameters["album_ids"] = string.Join(",", AlbumIDs);
            parameters["video_id"] = VideoID.ToString();
            parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Инициализирует экземпляр класса с заданными идентификаторами видеозаписи,
        /// её владельца и альбома.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <param name="albumID">Идентификатор альбома, в который нужно добавить видео.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoAddToAlbumRequest(ulong videoID, long ownerID, ulong albumID)
            : this(videoID, ownerID, albumID, null) { }

        /// <summary>
        /// Инициализирует экземпляр класса с заданными идентификаторами видеоазписи,
        /// её владельца и альбомов.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <param name="albumIDs">Идентификаторы альбомов, в которые нужно добавить видео.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoAddToAlbumRequest(ulong videoID, long ownerID, List<ulong> albumIDs)
            : this(videoID, ownerID, 0, albumIDs) { }

        /// <summary>
        /// Приватный конструктор. Получает данные от публичных конструкторов
        /// и заполняет поля.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <param name="albumID">Идентификатор альбома, в который нужно добавить видео.</param>
        /// <param name="albumIDs">Идентификаторы альбомов, в которые нужно добавить видео.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private VideoAddToAlbumRequest(ulong videoID, long ownerID, ulong? albumID, List<ulong> albumIDs)
        {
            VideoID = videoID;
            OwnerID = ownerID;
            if (albumID != null)
            {
                AlbumID = albumID.Value;
                IsSingle = true;
            }
            else
            {
                AlbumIDs = albumIDs;
                IsSingle = false;
            }
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoAddToAlbum; }
    }
}
