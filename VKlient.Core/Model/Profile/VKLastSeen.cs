using Newtonsoft.Json;
using System;
using OneVK.Enums.Common;
using OneVK.Core.Json;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Объект времени последнего посещения ВКонтакте.
    /// </summary>
    public sealed class VKLastSeen
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
