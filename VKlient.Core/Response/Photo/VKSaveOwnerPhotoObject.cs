using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Enums.Photo;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой содержимое ответа ВКонтакте
    /// на метод photo.saveOwnerPhoto.
    /// </summary>
    public class VKSaveOwnerPhotoObject
    {
        /// <summary>
        /// Параметр хэша, возвращаемый в результате загрузки файла на сервер.
        /// </summary>
        [JsonProperty("photo_hash")]
        public string PhotoHash { get; set; }
        /// <summary>
        /// Адрес фотографии среднего размера.
        /// </summary>
        [JsonProperty("photo_src")]
        public string MiddlePhotoSrc { get; set; }
        /// <summary>
        /// Адрес фотографии маленького размера.
        /// </summary>
        [JsonProperty("photo_src_small")]
        public string SmallPhotoSrc { get; set; }
        /// <summary>
        /// Адрес фотографии большого размера.
        /// </summary>
        [JsonProperty("photo_src_big")]
        public string BigPhotoSrc { get; set; }
        /// <summary>
        /// Сохранена ли фотография.
        /// </summary>
        [JsonProperty("saved")]
        public VKBoolean IsSaved { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("post_id")]
        public long PostID { get; set; }
    }
}
