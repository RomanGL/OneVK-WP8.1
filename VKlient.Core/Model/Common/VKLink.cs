using Newtonsoft.Json;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Ссылка из раздела ссылок ВКонтакте.
    /// </summary>
    public sealed class VKLink : IVKObject
    {
        /// <summary>
        /// Адрес ссылки.
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; set; }
        /// <summary>
        /// Заголовок ссылки.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Описание ссылки.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Квадратная картинка ссылки размером 50 пикс.
        /// </summary>
        [JsonProperty("image_src")]
        public string Photo50 { get; set; }
        /// <summary>
        /// Идентификатор ссылки.
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Имеет ли ссылка изображение.
        /// </summary>
        public bool HasImage { get { return !string.IsNullOrWhiteSpace(Photo50); } }
    }
}
