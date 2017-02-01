using Newtonsoft.Json;
using OneVK.Model.Audio;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервера ВКонтакте на запрос получения статуса.
    /// </summary>
    public class VKGetStatusResponse
    {
        /// <summary>
        /// Текст статуса.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Объект аудиозаписи, если включена трансляция аудио в статус.
        /// </summary>
        [JsonProperty("audio")]
        public VKAudio Audio { get; set; }
    }
}
