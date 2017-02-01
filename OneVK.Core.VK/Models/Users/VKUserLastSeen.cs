using Newtonsoft.Json;
using System;
using PropertyChanged;
using Microsoft.Practices.Prism.StoreApps;
using OneVK.Core.Models.Json;
using OneVK.Core.VK.Models.Common;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Объект времени последнего посещения ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKLastSeen : BindableBase
    {
        /// <summary>
        /// Платформа, с которой заходил пользователь.
        /// </summary>
        [JsonProperty("platform")]
        public VKPlatform Platform { get; set; }
        /// <summary>
        /// Время и дата последнего посещения.
        /// </summary>
        [JsonProperty("time")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Time { get; set; }
    }
}
