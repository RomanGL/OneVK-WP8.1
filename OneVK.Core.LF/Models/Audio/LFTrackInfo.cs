using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Core.Models.Json;

namespace OneVK.Core.LF.Models
{
    public class LFTrackInfo
    {
        /// <summary>
        /// Представляет информацию об исполнителе для класса информации о треке.
        /// </summary>
        public sealed class ArtistInfo
        {
            /// <summary>
            /// Имя исполнителя.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Идентификатор исполнителя.
            /// </summary>
            [JsonProperty("mbid")]
            public string MBID { get; set; }

            /// <summary>
            /// Ссылка на страницу с информацией об исполнителе.
            /// </summary>
            [JsonProperty("url")]
            public string InfoURL { get; set; }

            private ArtistInfo() { }
        }

        /// <summary>
        /// Представляет информацию об альбоме для класса информации о треке.
        /// </summary>
        public sealed class AlbumInfo
        {
            /// <summary>
            /// Заголовок альбома.
            /// </summary>
            [JsonProperty("title")]
            public string Title { get; set; }

            /// <summary>
            /// Идентификатор альбома.
            /// </summary>
            [JsonProperty("mbid")]
            public string MBID { get; set; }

            /// <summary>
            /// Ссылка на страницу с информацией об альбоме.
            /// </summary>
            [JsonProperty("url")]
            public string InfoURL { get; set; }

            /// <summary>
            /// Имя исполнителя.
            /// </summary>
            [JsonProperty("artist")]
            public string Artist { get; set; }

            /// <summary>
            /// Список изображений элемента.
            /// </summary>
            [JsonProperty("image")]
            public List<LFImage> Images { get; set; }

            /// <summary>
            /// Возвращает самое большое изображение элемента.
            /// </summary>
            public LFImage MaxImage
            {
                get
                {
                    if (Images == null || Images.Count == 0)
                        return null;
                    return Images.Last();
                }
            }

            private AlbumInfo() { }
        }

        /// <summary>
        /// Название элемента.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор элемента.
        /// </summary>
        [JsonProperty("mbid")]
        public string MBID { get; set; }

        /// <summary>
        /// Ссылка на страницу с информацией об элементе.
        /// </summary>
        [JsonProperty("url")]
        public string InfoURL { get; set; }

        /// <summary>
        /// Длительность композиции.
        /// </summary>
        [JsonConverter(typeof(SecondsToTimeSpanConverter))]
        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Количество воспроизведений.
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Количество слушателей.
        /// </summary>
        [JsonProperty("listeners")]
        public int ListenersCount { get; set; }

        /// <summary>
        /// Информация об исполнителе.
        /// </summary>
        [JsonProperty("artist")]
        public ArtistInfo Artist { get; set; }

        /// <summary>
        /// Информация об альбоме.
        /// </summary>
        [JsonProperty("album")]
        public AlbumInfo Album { get; set; }
    }
}
