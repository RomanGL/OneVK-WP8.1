using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Класс запроса для получения списка комментариев к видеозаписи.
    /// Это абстрактный класс.
    /// </summary>
    public abstract class VideoGetCommentsBaseRequest<T> : BaseVKCountedRequest<T>
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
        /// Порядок сортировки комментариев.
        /// </summary>
        public VKSortByDate Sort { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["video_id"] = VideoID.ToString();
            if (Sort != VKSortByDate.asc) parameters["sort"] = "desc";
            parameters["need_likes"] = "1";

            return parameters;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса
        /// c заданным идентификатором видеозаписи.
        /// </summary>
        /// <param name="query">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        protected VideoGetCommentsBaseRequest(ulong videoID)
        {
            VideoID = videoID;
            MaxCount = 100;
            DefaultCount = 20;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoGetComments; }
    }
}
