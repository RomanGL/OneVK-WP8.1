using Newtonsoft.Json;

namespace OneVK.Core.VK.Models.Audio
{
    /// <summary>
    /// Представляет собой альбом аудиозаписей ВКонтакте.
    /// </summary>
    public class VKAudioAlbum
    {
        /// <summary>
        /// Идентификатор альбома ВКонтакте.
        /// </summary>
        [JsonProperty("album_id")]
        public long ID { get; set; }
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
    }
}
