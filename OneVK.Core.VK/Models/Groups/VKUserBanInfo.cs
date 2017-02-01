using System;
using PropertyChanged;
using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using OneVK.Core.Models.Json;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Информация о блокировке пользователя в сообществе.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKUserBanInfo : BindableBase
    {
        /// <summary>
        /// Срок окончания блокировки.
        /// </summary>
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Комментарий к блокировке.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
