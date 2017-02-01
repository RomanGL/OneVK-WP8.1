using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет объект информации о городе ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public class VKCity : BindableBase
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [JsonProperty("id")]
        public uint ID { get; set; }
        
        /// <summary>
        /// Название.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
