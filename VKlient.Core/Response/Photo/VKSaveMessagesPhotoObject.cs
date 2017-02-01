using Newtonsoft.Json;
using OneVK.Enums.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой содержимое ответа ВКонткте
    /// на метод photo.saveMessagesPhoto.
    /// </summary>
    public class VKSaveMessagesPhotoObject
    {
        /// <summary>
        /// Фиг знает, что это ваще.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
        /// <summary>
        /// Идентификатор фотографии.
        /// </summary>
        [JsonProperty("pid")]
        public long PhotoID { get; set; }
        /// <summary>
        /// Идентификатор владельца фотографии.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }
        /// <summary>
        /// Фотография обычного (среднего) размера.
        /// </summary>
        [JsonProperty("src")]
        public string PhotoSrc { get; set; }
        /// <summary>
        /// Фотография маленького разера.
        /// </summary>
        [JsonProperty("src_small")]
        public string PhotoSmallSrc { get; set; }
        /// <summary>
        /// Фотография большого размера.
        /// </summary>
        [JsonProperty("src_big")]
        public string PhotoBigSrc { get; set; }
        /// <summary>
        /// Фотография очень большого размера.
        /// </summary>
        [JsonProperty("src_xbig")]
        public string PhotoXBigSrc { get; set; }
        /// <summary>
        /// Ваще ппц какая большая фотография.
        /// </summary>
        [JsonProperty("src_xxbig")]
        public string PhotoXXBigSrc { get; set; }
        /// <summary>
        /// Сохранена ли фотография.
        /// </summary>
        [JsonProperty("created")]
        public VKBoolean IsSaved { get; set; }
    }
}
