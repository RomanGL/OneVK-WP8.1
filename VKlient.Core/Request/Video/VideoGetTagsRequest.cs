using OneVK.Helpers;
using OneVK.Model.Common;
using OneVK.Response;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение списка отметок на видеозаписи.
    /// </summary>
    public class VideoGetTagsRequest : BaseVKCountedRequest<VKCountedItemsObject<VKTag>>
    {
        private ulong _videoID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID { get; set; }

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

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["video_id"] = VideoID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGetTags; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="video_id">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoGetTagsRequest(ulong videoID)
        {
            VideoID = videoID;
        }
    }
}
