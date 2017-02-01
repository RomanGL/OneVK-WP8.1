using Newtonsoft.Json;
using OneVK.Core.VK.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет информацию о комментариях ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKComments : BindableBase
    {
        /// <summary>
        /// Количество комментариев.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
        
        /// <summary>
        /// Может ли текущий пользователь комментировать запись.
        /// </summary>
        [JsonProperty("can_post")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanPost { get; set; }
    }
}
