using Newtonsoft.Json;
using System;
#if !PLAYER_TASK
using OneVK.Enums.App;

namespace OneVK.Core.Player
{
    /// <summary>
    /// Представляет аудиотрек.
    /// </summary>  
    public class AudioTrack : IAudioTrack
#else
namespace OneVK.BackgroundPlayer
{
    /// <summary>
    /// Представляет аудиотрек.
    /// </summary> 
    internal sealed class AudioTrack : IEquatable<AudioTrack>
#endif
    {
        /// <summary>
        /// Заголовок трека.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Исполнитель трека.
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; }
        /// <summary>
        /// Источник трека (URL или путь к файлу).
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
        /// <summary>
        /// Идентификатор текста аудиозаписи (если имеется).
        /// </summary>
        [JsonProperty("lyrics_id")]
        public long LyricsID { get; set; }
#if !PLAYER_TASK
        /// <summary>
        /// Тип содержимого файла.
        /// </summary>
        [JsonIgnore]
        public FileContentType ContentType { get { return FileContentType.Music; } }
        /// <summary>
        /// Расширение файла.
        /// </summary>
        [JsonIgnore]
        public string Extension { get { return ".mp3"; } }
        /// <summary>
        /// Имя результирующего файла.
        /// </summary>
        [JsonIgnore]
        public string FileName { get { return Title; } }

        /// <summary>
        /// Вовзвращает значение, равнли объекты.
        /// </summary>
        /// <param name="other">Объект для сравнения.</param>
        public bool Equals(IAudioTrack other)
        {
            return this.Title == other.Title &&
                this.Artist == other.Artist &&
                this.Source == other.Source;
        }
#else
        /// <summary>
        /// Вовзвращает значение, равнли объекты.
        /// </summary>
        /// <param name="other">Объект для сравнения.</param>
        public bool Equals(AudioTrack other)
        {
            return this.Title == other.Title &&
                this.Artist == other.Artist &&
                this.Source == other.Source;
        }
#endif
    }
}
