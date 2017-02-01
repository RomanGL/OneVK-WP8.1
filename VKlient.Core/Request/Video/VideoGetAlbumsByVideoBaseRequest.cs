using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка альбомов, в которых находится видеозапись.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class VideoGetAlbumsByVideoBaseRequest<T> : BaseVKCountedRequest<T>
    {
        private long _ownerID;
        private ulong _videoID;

        /// <summary>
        /// Идентификатор владельца альбома.
        /// </summary>
        public long TargetID { get; set; }

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
        /// <exception cref="ArgumentOutOfRangeException"/>
        protected VideoGetAlbumsByVideoBaseRequest(ulong videoID, long ownerID)
        {
            VideoID = videoID;
            OwnerID = ownerID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (TargetID != 0) parameters["target_id"] = TargetID.ToString();
            parameters["video_id"] = VideoID.ToString();
            parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGetAlbumsByVideo; }
    }
}
