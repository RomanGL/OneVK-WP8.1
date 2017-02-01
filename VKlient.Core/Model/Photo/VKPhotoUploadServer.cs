using System;
using OneVK.Model.Common;
using Newtonsoft.Json;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Объект сервера для выгрузки фотографий ВКонтакте.
    /// </summary>
    public class VKPhotoUploadServer : IVKUploadServer
    {
        /// <summary>
        /// Ссылка для выгрузки.
        /// </summary>
        [JsonProperty("upload_url")]
        public string UploadURL { get; set; }

        /// <summary>
        /// Идентификатор альбома, в который будет загружена фотография.
        /// </summary>
        [JsonProperty("album_id")]
        public long AlbumID { get; set; }

        /// <summary>
        /// Идентификатор пользователя, от чьего имени будет загружено фото.
        /// </summary>
        [JsonProperty("user_id")]
        public long ID { get; set; }
    }
}
