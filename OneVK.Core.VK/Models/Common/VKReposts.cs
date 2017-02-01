using Newtonsoft.Json;
using OneVK.Core.VK.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет информацию о репостах.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKReposts : BindableBase
    {
        /// <summary>
        /// Количество репостов.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
        
        /// <summary>
        /// Скопировал ли текущий пользователь.
        /// </summary>
        [JsonProperty("user_reposted")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool UserReposted { get; set; }
    }
}
