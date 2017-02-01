using System;
using OneVK.Enums.Audio;
using OneVK.Core.Json;
using Newtonsoft.Json;
using OneVK.Core.Player;
using OneVK.Enums.App;
using OneVK.Core.Transfer;
#if ONEVK_CORE
using GalaSoft.MvvmLight;
#endif

namespace OneVK.Model.Audio
{
    /// <summary>
    /// Аудиозапись ВКонтакте.
    /// </summary>
#if ONEVK_CORE
    public class VKAudio : ObservableObject, IAudioTrack
#else
    public class VKAudio
#endif
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

#if ONEVK_CORE
        private bool _isDeleted;

        /// <summary>
        /// Является ли данная запись удаленной пользователем.
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { Set(() => IsDeleted, ref _isDeleted, value); }
        }

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
#endif

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
    }
}