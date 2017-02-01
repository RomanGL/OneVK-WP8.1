using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Контакт раздела контактов ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKContact : BindableBase
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [JsonProperty("user_id")]
        public long UserID { get; set; }
        
        /// <summary>
        /// Описание контакта.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Электронная почта контакта.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        
        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        [JsonProperty("mobile_phone")]
        public string Phone { get; set; }
    }
}
