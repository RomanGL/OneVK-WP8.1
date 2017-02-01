using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Представляет собой телефонные номера пользователя ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKUserContacts : BindableBase
    {
        /// <summary>
        /// Мобильный телефон пользователя.
        /// </summary>
        [JsonProperty("mobile_phone")]
        public string MobilePhone { get; set; }
        
        /// <summary>
        /// Домашний телефон пользователя.
        /// </summary>
        [JsonProperty("home_phone")]
        public string HomePhone { get; set; }
    }
}
