using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Enums.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос получения текущего статуса и даты последней активности указанного пользователя. 
    /// </summary>
    public class VKGetLastActivityObject
    {
        /// <summary>
        /// Текущий статус пользователя (1 — в сети, 0 — не в сети).
        /// </summary>
        [JsonProperty("online")]
        public VKBoolean Online { get; set; }

        /// <summary>
        /// Дата последней активности пользователя в формате unixtime.
        /// </summary>
        [JsonProperty("time")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public long Time { get; set; }
    }
}
