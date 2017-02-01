using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на восстановление удаленной видеозаписи.
    /// </summary>
    public class VideoRestoreRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _videoID;

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
        /// Идентификатор пользователя или сообщества, которому принадлежит видеозапись.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором видеозаписи.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoRestoreRequest(ulong videoID)
        {
            VideoID = videoID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["video_id"] = VideoID.ToString();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoRestore; }
    }
}
