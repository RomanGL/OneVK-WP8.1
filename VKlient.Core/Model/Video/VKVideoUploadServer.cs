using Newtonsoft.Json;
using OneVK.Model.Common;

namespace OneVK.Model.Video
{
    /// <summary>
    /// Объект сервера для отправки видеозаписей ВКонтакте.
    /// </summary>
    public class VKVideoUploadServer : VKUploadServerBase
    {
        /// <summary>
        /// Идентификатор видеозаписи.
        /// </summary>
        [JsonProperty("video_id")]
        public long ID { get; set; }

        /// <summary>
        /// Название видеозаписи.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Описание видеозаписи.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор пользователя или сообщества.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }
    }
}
