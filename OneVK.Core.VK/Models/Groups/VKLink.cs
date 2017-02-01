using Microsoft.Practices.Prism.StoreApps;
using Newtonsoft.Json;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Ссылка из раздела ссылок ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKLink : BindableBase
    {
        /// <summary>
        /// Адрес ссылки.
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; set; }
        /// <summary>
        /// Заголовок ссылки.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Описание ссылки.
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }
        /// <summary>
        /// Квадратная картинка ссылки размером 50 пикс.
        /// </summary>
        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
        /// <summary>
        /// Квадратная картинка ссылки размером 100 пикс.
        /// </summary>
        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }
        /// <summary>
        /// Идентификатор ссылки.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
    }
}
