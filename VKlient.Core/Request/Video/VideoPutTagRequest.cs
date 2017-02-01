using OneVK.Helpers;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на добавление отметки на видеозапись.
    /// </summary>
    public class VideoPutTagRequest : BaseVKRequest<ulong>
    {
        private ulong _userID, _videoID;

        /// <summary>
        /// Идентификатор пользователя, которого нужно отметить.
        /// </summary>
        public ulong UserID
        {
            get { return _userID; }
            private set
            {
                DataValidationHelper.CheckEqualZero(value);
                _userID = value;
            }
        }

        /// <summary>
        /// Идентификатор владельца видеозаписи.
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
        /// Текст отметки.
        /// </summary>
        public string TaggedName { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданными идентификаторами видеозаписи и
        /// пользователя, которого нужно на ней отметить.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя, которого нужно отметить.</param>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoPutTagRequest(ulong userID, ulong videoID)
        {
            UserID = userID;
            VideoID = videoID;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            parameters["user_id"] = UserID.ToString();
            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["video_id"] = VideoID.ToString();
            if (!String.IsNullOrWhiteSpace(TaggedName)) parameters["tagged_name"] = TaggedName;

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoPutTag; }
    }
}
