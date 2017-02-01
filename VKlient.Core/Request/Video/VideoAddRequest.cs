using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добавление видеозаписи в список пользователя или сообщества.
    /// </summary>
    public class VideoAddRequest : BaseVKRequest<long>
    {
        private long _ownerID;
        private ulong _videoID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, в которое нужно добавить видео.
        /// </summary>
        public long TargetID { get; set; }

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
        public override string GetMethod() { return VKMethodsConstants.VideoAdd; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с идентификатором видеозаписи
        /// и её владельца для добавления видеозаписи в коллекцию пользователя.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoAddRequest(ulong videoID, long ownerID)
        {
            Initialize(videoID, ownerID, null);
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса с идентификаторами видеозаписи,
        /// её владельца и пользователя или сообщества, в коллекцию которого её нужно добавить.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <param name="targetID">Идентификатор пользователя или сообщества, в коллекцию 
        /// которого необходимо добавить видеозапись.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoAddRequest(ulong videoID, long ownerID, long targetID)
        {
            Initialize(videoID, ownerID, targetID);
        }

        /// <summary>
        /// Инициализирует поля класса.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <param name="targetID">Идентификатор пользователя или сообщества, в коллекцию 
        /// которого необходимо добавить видеозапись.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Initialize(ulong videoID, long ownerID, long? targetID)
        {
            VideoID = videoID;
            OwnerID = ownerID;
            if (targetID.HasValue)
                TargetID = targetID.Value;
        }
    }
}
