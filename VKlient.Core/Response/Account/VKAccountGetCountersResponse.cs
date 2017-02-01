using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервера ВКонтакте на запрос списка счетчиков текущего пользователя.
    /// </summary>
    public class VKAccountGetCountersResponse
    {
        /// <summary>
        /// Счетчик новых сообщений.
        /// </summary>
        [JsonProperty("messages")]
        public int Messages { get; set; }
        /// <summary>
        /// Счетчик заявок в друзья.
        /// </summary>
        [JsonProperty("friends")]
        public int Friends { get; set; }
        /// <summary>
        /// Счетчик новых фотографий.
        /// </summary>
        [JsonProperty("photos")]
        public int Photos { get; set; }
        /// <summary>
        /// Счетчик новых видеозаписей.
        /// </summary>
        [JsonProperty("videos")]
        public int Videos { get; set; }
        /// <summary>
        /// Счетчик заметок.
        /// </summary>
        [JsonProperty("notes")]
        public int Notes { get; set; }
        /// <summary>
        /// Счетчик новых подарков.
        /// </summary>
        [JsonProperty("gifts")]
        public int Gifts { get; set; }
        /// <summary>
        /// Счетчик новых событий.
        /// </summary>
        [JsonProperty("events")]
        public int Events { get; set; }
        /// <summary>
        /// Счетчик приглашений в сообщества.
        /// </summary>
        [JsonProperty("groups")]
        public int Groups { get; set; }
        /// <summary>
        /// Счетчик уведомлений.
        /// </summary>
        [JsonProperty("notifications")]
        public int Notifications { get; set; }
    }
}
