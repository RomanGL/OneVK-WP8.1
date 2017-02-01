using Microsoft.Practices.Prism.StoreApps;
using Newtonsoft.Json;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Представляет элемент тематики сообщества.
    /// </summary>
    public sealed class VKGroupSubject : BindableBase
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [JsonProperty("id")]
        public uint ID { get; set; }
        /// <summary>
        /// Название.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
