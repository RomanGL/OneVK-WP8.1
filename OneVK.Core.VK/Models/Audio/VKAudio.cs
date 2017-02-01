using System;
using Newtonsoft.Json;
using OneVK.Core.Models.Json;
using OneVK.Core.Models;

namespace OneVK.Core.VK.Models.Audio
{
    /// <summary>
    /// Аудиозапись ВКонтакте.
    /// </summary>
    public sealed class VKAudio : IAudioTrack, IVKAudioTrack, IVKMediaItem
    {
        /// <summary>
        /// Идентификатор аудиозаписи.
        /// </summary>
        [JsonProperty("id")]
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
        public string Source { get; set; }

        /// <summary>
        /// Идентификатор текста аудиозаписи.
        /// </summary>
        [JsonProperty("lyrics_id")]
        public long LyricsID { get; set; }

        /// <summary>
        /// Идентификатор альбома, в котором находится аудиозапись.
        /// </summary>
        [JsonProperty("album_id")]
        public long AlbumID { get; set; }

        /// <summary>
        /// Жанр аудиозаписи.
        /// </summary>
        [JsonProperty("genre_id")]
        public VKAudioGenre Genre { get; set; }

        /// <summary>
        /// Возвращает тип медиаэлемента ВКонтакте.
        /// </summary>
        [JsonProperty("type")]
        public VKMediaType Type { get { return VKMediaType.Audio; } }

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