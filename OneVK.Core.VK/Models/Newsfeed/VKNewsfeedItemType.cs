using Newtonsoft.Json;

namespace OneVK.Core.VK.Models.Newsfeed
{
    /// <summary>
    /// Тип новости ленты новостей ВКонтакте.
    /// </summary>
    public enum VKNewsfeedItemType : byte
    {
        /// <summary>
        /// Неизвестный тип записи.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Новая запись со стены.
        /// </summary>
        [JsonProperty("post")]
        Post,
        /// <summary>
        /// Новая фотография.
        /// </summary>
        [JsonProperty("photo")]
        Photo,
        /// <summary>
        /// Новая отметка на фотографии.
        /// </summary>
        [JsonProperty("photo_tag")]
        PhotoTag,
        /// <summary>
        /// Новая фотография на стене.
        /// </summary>
        [JsonProperty("wall_photo")]
        WallPhoto,
        /// <summary>
        /// Новый друг.
        /// </summary>
        [JsonProperty("friend")]
        Friend,
        /// <summary>
        /// Новая заметка.
        /// </summary>
        [JsonProperty("note")]
        Note
    }
}
