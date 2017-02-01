using Newtonsoft.Json;
using OneVK.Core.Transfer;
using System;

namespace OneVK.Core.Player
{
    /// <summary>
    /// Представляет интерфейс аудиотрека.
    /// </summary>
    public interface IAudioTrack : IEquatable<IAudioTrack>, IDownloadSupport
    {
        /// <summary>
        /// Заголовок трека.
        /// </summary>
        [JsonProperty("title")]
        string Title { get; set; }
        /// <summary>
        /// Имя исполнителя трека.
        /// </summary>
        [JsonProperty("artist")]
        string Artist { get; set; }
        /// <summary>
        /// Идентификатор текста аудиозаписи (если имеется).
        /// </summary>
        [JsonProperty("lyrics_id")]
        long LyricsID { get; set; }
    }
}
