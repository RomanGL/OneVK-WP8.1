using Newtonsoft.Json;

namespace OneVK.Model.Audio
{
    /// <summary>
    /// Представляет собой альбом аудиозаписей ВКонтакте.
    /// </summary>
    public class VKAudioAlbum : IVKObject
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
