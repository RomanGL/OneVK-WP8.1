using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на исключение видеозаписи из альбома.
    /// </summary>
    public class VideoRemoveFromAlbumRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _ownerID;
        private ulong _albumID, _videoID;
        private List<ulong> _albumIDs;
        private bool _isSingle = true;

        public bool IsSingle
        {
            get { return _isSingle; }
            set { _isSingle = value; }
        }
        
        /// <summary>
        /// Идентификатор владельца альбома.
        /// </summary>
        public long TargetID { get; set; }

        /// <summary>
        /// Идентификатор альбома.
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
        /// Инициализирует новый экземпляр класса с заданными идентификаторами видеозаписи
        /// и ее владельца.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <param name="albumID">Идентификатор альбома.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoRemoveFromAlbumRequest(ulong videoID, long ownerID, ulong albumID)
        {
            VideoID = videoID;
            OwnerID = ownerID;
            AlbumID = albumID;
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
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoRemoveFromAlbum; }
    }
}
