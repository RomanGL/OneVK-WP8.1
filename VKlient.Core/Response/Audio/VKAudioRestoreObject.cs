using Newtonsoft.Json;
using System;
using OneVK.Core.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой содержимое ответа сервера ВКонтакте
    /// на вызов метода audio.restore.
    /// </summary>
    public class VKAudioRestoreObject
    {
        /// <summary>
        /// Идентификатор аудиозаписи.
        /// </summary>
        [JsonProperty("aid")]
        public long ID { get; set; }

        /// <summary>
        /// Идентификатор владельца аудиозаписи.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }

        /// <summary>
        /// Исполнитель аудиозаписи.
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// Заголовок аудиозаписи.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Длительность аудиозаписи.
        /// </summary>
        [JsonProperty("duration")]
        [JsonConverter(typeof(SecondsToTimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Ссылка на MP3-файл аудиозаписи.
        /// </summary>
        [JsonProperty("url")]
        public string URL { get; set; }

        /// <summary>
        /// Идентификатор текста аудиозаписи.
        /// </summary>
        [JsonProperty("lyrics_id")]
        public long LyricsID { get; set; }
    }
}
