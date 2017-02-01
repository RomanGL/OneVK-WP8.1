using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на жалобу на видеозапись.
    /// </summary>
    public class VideoReportRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _ownerID;
        private ulong _videoID;

        /// <summary>
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
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
        /// Тип жалобы.
        /// </summary>
        public VKReason Reason { get; set; }

        /// <summary>
        /// Комментарий для жалобы.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Поисковой запрос, если видеозапись была найдена через поиск.
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="ownerID">Идентификатор пользователя или сообщества, которому принадлежит видеозапись.</param>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoReportRequest(long ownerID, ulong videoID)
        {
            OwnerID = ownerID;
            VideoID = videoID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["owner_id"] = OwnerID.ToString();
            parameters["video_id"] = VideoID.ToString();
            if (Reason != VKReason.Spam) parameters["reason"] = ((byte)Reason).ToString();
            if (!String.IsNullOrWhiteSpace(Comment)) parameters["comment"] = Comment;
            if (!String.IsNullOrWhiteSpace(SearchQuery)) parameters["search_query"] = SearchQuery;

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoReport; }
    }
}
