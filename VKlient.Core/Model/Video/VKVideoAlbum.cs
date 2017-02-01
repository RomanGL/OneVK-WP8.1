using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace OneVK.Model.Video
{
    /// <summary>
    /// Представляет собой альбом видеозаписей ВКонтакте.
    /// </summary>
#if ONEVK_CORE
    public class VKVideoAlbum : ObservableObject, IVKObject
#else
    public class VKVideoAlbum : IVKObject
#endif
    {
        /// <summary>
        /// Идентификатор альбома ВКонтакте.
        /// </summary>
        [JsonProperty("id")]
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
