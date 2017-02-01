using OneVK.Enums.Common;
using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на удаление отметки с видеозаписи.
    /// </summary>
    public class VideoRemoveTagRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private ulong _tagID, _videoID;

        /// <summary>
        /// Идентификатор отметки.
        /// </summary>
        public ulong TagID
        {
            get { return _tagID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _tagID = value;
            }
        }

        /// <summary>
        /// Идентификатор владельца видеозаписи (пользователь или сообщество).
        /// </summary>
        public ulong OwnerID { get; set; }

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

            parameters["tag_id"] = TagID.ToString();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["video_id"] = VideoID.ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoRemoveTag; }

        /// <summary>
        /// Базовый конструктор.
        /// </summary>
        /// <param name="tagID">Идентификатор отметки.</param>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public VideoRemoveTagRequest(ulong tagID, ulong videoID)
        {
            TagID = tagID;
            VideoID = videoID;
        }
    }
}
