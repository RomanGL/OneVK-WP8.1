using Newtonsoft.Json;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Представляет собой телефонные номера пользователя ВКонтакте.
    /// </summary>
    public class VKUserContacts
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
