using System.Collections.Generic;
using OneVK.Enums.Video;
using OneVK.Helpers;
using OneVK.Enums.Common;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на редактирование данных видеозаписи на странице пользователя или сообщества.
    /// </summary>
    public class VideoEditRequest : BaseVKRequest<VKOperationIsSuccess>
    {
        private long _videoID;

        /// <summary>
        /// Идентификатор владельца видеозаписи.
        /// </summary>
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор видеозаписи.
        /// </summary>
        public long VideoID
        {
            get { return _videoID; }
            private set
            {
                DataValidationHelper.CheckGreaterThanZero(value);
                _videoID = value;
            }
        }

        /// <summary>
        /// Новое название для видеозаписи.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Новое описание для видеозаписи.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Зацикливание воспроизведения видеозаписи.
        /// </summary>
        public VKVideoRepeat Repeat { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным идентификатором видеозаписи
        /// и неизвестным значением ее зацикливания воспроизведения.
        /// </summary>
        /// <param name="videoID">Идентификатор видеозаписи.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public VideoEditRequest(long videoID)
        {
            VideoID = videoID;
            Repeat = VKVideoRepeat.Unknown;
        }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (OwnerID != 0) parameters["owner_id"] = OwnerID.ToString();
            parameters["video_id"] = VideoID.ToString();
            if (!string.IsNullOrWhiteSpace(Name)) parameters["name"] = Name;
            if (!string.IsNullOrWhiteSpace(Description)) parameters["desc"] = Description;
            if (Repeat != VKVideoRepeat.Unknown) parameters["repeat"] = ((byte)Repeat).ToString();

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoEdit; }
    }
}
