using OneVK.Enums.Common;
using OneVK.Helpers;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление видеозаписи со страницы пользователя.
    /// </summary>
    public class VideoDeleteRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _videoID;
        private long _ownerID;

        /// <summary>
        /// Идентификатор видеозаписи.
        /// </summary>
        public ulong VideoID
        {
            get { return _videoID; }
            private set
            {
                DataValidationHelper.CheckGreaterThanZero(value);
                _videoID = value;
            }
        }

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID
        {
            get { return _ownerID; }
            set
            {
                DataValidationHelper.CheckEqualZero(value);
                _ownerID = value;
            }
        }
        
        /// <summary>
        /// Идентификатор пользователя или сообщества, для которого нужно удалить видеозапись.
        /// </summary>
        public long TargetID { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором видеозаписи.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoDeleteRequest(ulong videoID, long ownerID, long targetID)
        {
            VideoID = videoID;
            OwnerID = ownerID;
            TargetID = targetID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["video_id"] = VideoID.ToString();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            if (TargetID != 0) parameters["target_id"] = TargetID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoDelete; }
    }
}
