using Newtonsoft.Json;

namespace OneVK.Model.Doc
{
    /// <summary>
    /// Представляет документ ВКонтакте.
    /// </summary>
    public class VKDocument : IVKObject
    {
        /// <summary>
        /// Идентификатор документа.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Идентификатор владельца документа.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }

        /// <summary>
        /// Заголовок документа.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Размер документа в байтах.
        /// </summary>
        [JsonProperty("size")]
        public long SizeInBytes { get; set; }

        /// <summary>
        /// Расширение документа.
        /// </summary>
        [JsonProperty("ext")]
        public string Extension { get; set; }

        /// <summary>
        /// Прямая ссылка на файл.
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; set; }

        /// <summary>
        /// Адрес изображения с размером 100x75px (если файл графический).
        /// </summary>
        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }

        /// <summary>
        /// Адрес изображения с размером 130x100px (если файл графический).
        /// </summary>
        [JsonProperty("photo_130")]
        public string Photo130 { get; set; }
    }
}
