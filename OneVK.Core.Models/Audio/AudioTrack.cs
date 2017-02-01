using Newtonsoft.Json;
using System;
using OneVK.Core.Models.Json;

namespace OneVK.Core.Models
{
    /// <summary>
    /// Представляет аудиотрек проигрывателя ВКачай.
    /// </summary>
    public sealed class AudioTrack : IAudioTrack, IVKAudioTrack, IVKMediaItem, IEquatable<IAudioTrack>, IEquatable<IVKAudioTrack>
    {
        /// <summary>
        /// Возвращает или задает заголовок трека.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Возвращает или задает имя исполнителя трека.
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// Возвращает или задает источник для файла.
        /// </summary>
        [JsonProperty("url")]
        public string Source { get; set; }

        /// <summary>
        /// Возвращает тип медиаэлемента ВКонтакте.
        /// </summary>
        [JsonProperty("type")]
        public VKMediaType Type { get { return VKMediaType.Audio; } }

        /// <summary>
        /// Возвращает или задает идентификатор владельца аудиозаписи ВКонтакте.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор аудиозаписи ВКонтакте.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор текста аудиозаписи ВКонтакте.
        /// </summary>
        [JsonProperty("lyrics_id")]
        public long LyricsID { get; set; }

        /// <summary>
        /// Возвращает или задает длительность аудиозаписи.
        /// </summary>
        [JsonProperty("duration")]
        [JsonConverter(typeof(SecondsToTimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Сравнивает два трека на равенство.
        /// </summary>
        /// <param name="other">Другой трек.</param>
        public bool Equals(IAudioTrack other)
        {
            if (ReferenceEquals(this, other)) return true;

            return this.Title == other.Title &&
                this.Artist == other.Artist &&
                this.Source == other.Source;
        }

        /// <summary>
        /// Сравнивает два трека на равенство.
        /// </summary>
        /// <param name="other">Другой трек.</param>
        public bool Equals(IVKAudioTrack other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (this.OwnerID == 0 || other.OwnerID == 0 ||
                this.ID == 0 || other.ID == 0)
                return this.Equals((IAudioTrack)other);
            else return this.OwnerID == other.OwnerID && this.ID == other.ID;
        }
    }
}
