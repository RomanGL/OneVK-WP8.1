using Microsoft.Practices.Prism.StoreApps;
using Newtonsoft.Json;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Groups
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
        [JsonProperty("desc")]
        public string Description { get; set; }
        /// <summary>
        /// Электронная почта контакта.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}
