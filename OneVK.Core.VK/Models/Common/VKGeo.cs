using Newtonsoft.Json;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет собой информацию о местоположении ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKGeo
    {
        /// <summary>
        /// Тип места.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Описание места (если добавлено).
        /// </summary>
        [JsonProperty("place")]
        public VKPlace Place { get; set; }
    }
}
