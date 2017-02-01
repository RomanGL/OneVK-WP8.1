using Newtonsoft.Json;
using OneVK.Enums.Common;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Представляет собой альбом фотографий ВКонтакте.
    /// </summary>
    public class VKPhotoAlbum : IVKObject
    {
        /// <summary>
        /// Идентификатор альбома ВКонтакте.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
        /// <summary>
        /// Идентификатор фотографии, которая является обложкой.
        /// </summary>
        [JsonProperty("thumb_id")]
        public long ThumbID { get; set; }
        /// <summary>
        /// Путь к фотографии, которая является обложкой альбома.
        /// </summary>
        [JsonProperty("thumb_src")]
        public string Thumbnail { get; set; }
        /// <summary>
        /// Идентификатор владельца альбома.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }
        /// <summary>
        /// Название альбома.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Описание альбома.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Количество фотографий в альбоме.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }
        /// <summary>
        /// Настройки приватности для просмотра альбома (только для альбома пользователя).
        /// </summary>
        [JsonProperty("privacy_view")]
        public VKAlbumPrivacy PrivacyView { get; set; }
        /// <summary>
        /// Настройки приватности для комментирования альбома (только для альбома пользователя).
        /// </summary>
        [JsonProperty("privacy_comment")]
        public VKAlbumPrivacy PrivacyComment { get; set; }
        /// <summary>
        /// Кто может загружать фотографии в альбом (только для альбома сообщества).
        /// </summary>
        [JsonProperty("upload_by_admins_only")]
        public VKBoolean UploadByAdminsOnly { get; set; }
        /// <summary>
        /// Отключено ли комментирование альбома (только для альбома сообщества).
        /// </summary>
        [JsonProperty("comments_disabled")]
        public VKBoolean CommentsDisabled { get; set; }
        /// <summary>
        /// Может ли текущий пользователь добавлять фотографии в альбом.
        /// </summary>
        [JsonProperty("can_upload")]
        public VKBoolean CanUpload { get; set; }
    }
}
