using OneVK.Enums.Common;
using OneVK.Model.Video;
using System;
using System.Collections.Generic;

namespace OneVK.Request
{
    /// <summary>
    /// Представляет запрос на получение адреса сервера (необходимого для загрузки) и данные видеозаписи. 
    /// </summary>
    public class VideoSaveRequest : BaseVKRequest<VKVideoUploadServer>
    {
        /// <summary>
        /// Название видеофайла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание видеофайла.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Относится ли видеозапись к личному сообщению.
        /// </summary>
        public VKBoolean IsPrivate { get; set; }

        /// <summary>
        /// Опулбликовать ли видеозапись после ее сохранения на стене.
        /// </summary>
        public VKBoolean Wallpost { get; set; }

        /// <summary>
        /// URL для встраивания видео с внешнего сайта.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Идентификатор сообщества, в которое будет сохранен видеофайл.
        /// </summary>
        public ulong GroupID { get; set; }

        /// <summary>
        /// Идентификатор альбома, в который будет загружен видеофайл.
        /// </summary>
        public ulong AlbumID  { get; set; }

        /// <summary>
        /// Зацикливание воспроизведения видеозаписи.
        /// </summary>
        public VKBoolean Repeat { get; set; }

        /// <summary>
        /// Возвращает коллекцию параметров.
        /// </summary>
        public override Dictionary<string, string> GetParameters()
        {
            var parameters = base.GetParameters();

            if (!String.IsNullOrWhiteSpace(Name))
                parameters["name"] = Name;
            else
                parameters["name"] = "No name";
            if (!String.IsNullOrWhiteSpace(Description)) parameters["description"] = Description;
            if (IsPrivate != VKBoolean.False) parameters["is_private"] = "1";
            if (Wallpost != VKBoolean.False) parameters["wallpost"] = "1";
            if (!String.IsNullOrWhiteSpace(Link)) parameters["link"] = Link;
            if (GroupID != 0) parameters["group_id"] = GroupID.ToString();
            if (AlbumID != 0) parameters["album_id"] = AlbumID.ToString();
            if (Repeat != VKBoolean.False) parameters["repeat"] = "1";

            return parameters;
        }

        /// <summary>
        /// Возвращает связанный с запросом метод.
        /// </summary>
        public override string GetMethod() { return VKMethodsConstants.VideoSave; }
    }
}
