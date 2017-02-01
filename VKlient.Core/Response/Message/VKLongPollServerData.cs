using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервера ВКонтакте на запрос данных для 
    /// подключения к Long Poll серверу.
    /// </summary>
    public sealed class VKLongPollServerData
    {
        /// <summary>
        /// Секретный ключ сессии.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// Адрес сервера, к которому нужно отправлять запрос.
        /// </summary>
        [JsonProperty("server")]
        public string Server { get; set; }

        /// <summary>
        /// Номер последнего события, начиная с которого Вы хотите получать данные.
        /// </summary>
        [JsonProperty("ts")]
        public string Ts { get; set; }

        /// <summary>
        /// Последнее значение параметра new_pts, полученное от Long Poll сервера, 
        /// используется для получения действий, которые хранятся всегда.
        /// </summary>
        [JsonProperty("pts")]
        public string Pts { get; set; }
    }
}
