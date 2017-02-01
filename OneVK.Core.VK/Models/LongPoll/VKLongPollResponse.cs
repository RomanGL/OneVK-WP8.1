using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет ответ LongPoll сервера ВКонтакте.
    /// </summary>
    public sealed class VKLongPollResponse
    {
        /// <summary>
        /// Информация о произошедшей ошибке.
        /// </summary>
        [JsonProperty("failed")]
        public VKLongPollErrors Error { get; set; }

        /// <summary>
        /// Последнее событие, начиная с которого требуется получить обновления.
        /// </summary>
        [JsonProperty("ts")]
        public string Ts { get; set; }

        /// <summary>
        /// Коллекция обновлений.
        /// </summary>
        [JsonProperty("updates")]
        public List<IVKLongPollUpdate> Updates { get; set; }
    }
}
