using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой содержимое ответа сервера ВКонтакте
    /// на метод video.addAlbum.
    /// </summary>
    public class VKVideoAddAlbumObject
    {
        /// <summary>
        /// Идентификатор альбома.
        /// </summary>
        [JsonProperty("album_id")]
        public long AlbumID { get; set; }
    }
}
