using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой содержимое ответа сервера ВКонтакте
    /// на метод audio.addAlbum.
    /// </summary>
    public class VKAudioAddAlbumObject
    {
        /// <summary>
        /// Идентификатор альбома.
        /// </summary>
        [JsonProperty("album_id")]
        public long AlbumID { get; set; }
    }
}
