using Newtonsoft.Json;

namespace OneVK.Model.Audio
{
    /// <summary>
    /// Преставляет собой объект текста аудиозаписи ВКонтакте.
    /// </summary>
    public class VKAudioLyrics : IVKObject
    {
        /// <summary>
        /// Идентификатор текста аудиозаписи.
        /// </summary>
        [JsonProperty("lyrics_id")]
        public long ID { get; set; }
        /// <summary>
        /// Текст аудиозаписи.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
