using OneVK.Model;
using System;
using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Core;
#if ONEVK_CORE
using GalaSoft.MvvmLight;
#endif

namespace OneVK.Model.Video
{
    /// <summary>
    /// Базовая видеозапись ВКонтакте.
    /// </summary>
#if ONEVK_CORE
    public class VKVideoBase : ObservableObject, IVKVideoBase, IThumbnailSupport
#else
    public class VKVideoBase : IVKVideoBase
#endif    
    {       
        /// <summary>
        /// Идентификатор владельца видеозаписи.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }
        /// <summary>
        /// Заголовок видеозаписи.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Описание видеозаписи.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Длительность видеозаписи.
        /// </summary>
        [JsonProperty("duration")]
        [JsonConverter(typeof(SecondsToTimeSpanConverter))]
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Ссылка на файл MP4 240p.
        /// </summary>
        [JsonProperty("url_240")]
        public string URL240 { get; set; }
        /// <summary>
        /// Ссылка на файл MP4 360p.
        /// </summary>
        [JsonProperty("url_360")]
        public string URL360 { get; set; }
        /// <summary>
        /// Ссылка на файл MP4 480p.
        /// </summary>
        [JsonProperty("url_480")]
        public string URL480 { get; set; }
        /// <summary>
        /// Ссылка на файл MP4 720p.
        /// </summary>
        [JsonProperty("url_720")]
        public string URL720 { get; set; }
        /// <summary>
        /// Ссылка на картинку шириной 130 пикс.
        /// </summary>
        [JsonProperty("photo_130")]
        public string Photo130 { get; set; }
        /// <summary>
        /// Ссылка на картинку шириной 320 пикс.
        /// </summary>
        [JsonProperty("photo_320")]
        public string Photo320 { get; set; }
        /// <summary>
        /// Ссылка на картинку шириной 640 пикс.
        /// </summary>
        [JsonProperty("photo_640")]
        public string Photo640 { get; set; }
        /// <summary>
        /// Дата создания видеозаписи.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }
        /// <summary>
        /// Дата добавления видеозаписи.
        /// </summary>
        [JsonProperty("adding_date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime AddingDate { get; set; }
        /// <summary>
        /// Количество просмотров.
        /// </summary>
        [JsonProperty("views")]
        public long ViewsCount { get; set; }
        /// <summary>
        /// Количество комментариев.
        /// </summary>
        [JsonProperty("comments")]
        public long CommentsCount { get; set; }
        /// <summary>
        /// Ссылка на HTML5-проигрыватель.
        /// </summary>
        [JsonProperty("player")]
        public string Player { get; set; }
        /// <summary>
        /// Идентификатор видеозаписи.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }
        /// <summary>
        /// Ключ прямого доступа к закрытым объектам.
        /// </summary>
        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

#if ONEVK_CORE
        private bool _isDeleted;

        /// <summary>
        /// Является ли данная видеозапись удалённой пользователем.
        /// </summary>
        [JsonIgnore]
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { Set(() => IsDeleted, ref _isDeleted, value); }
        }

        /// <summary>
        /// Имеется ли в видеозаписи описание.
        /// </summary>
        [JsonIgnore]
        public bool HasDescription { get { return !string.IsNullOrEmpty(Description); } }

        /// <summary>
        /// Имеется ли у видеозаписи дата добавления.
        /// </summary>
        [JsonIgnore]
        public bool HasAddingDate { get { return AddingDate.Ticks != 0; } }

        /// <summary>
        /// Возвращает ссылку на изображение видеозаписи в максимальном качестве.
        /// </summary>
        [JsonIgnore]
        public string MaxPhoto
        {
            get
            {
                if (!string.IsNullOrEmpty(Photo640))
                    return Photo640;
                else if ((!string.IsNullOrEmpty(Photo320)))
                    return Photo320;
                else
                    return Photo130;
            }
        }
#endif

        #region IThumbnailSupport
        /// <summary>
        /// Данные для визуализации миниатюры.
        /// </summary>
        [JsonIgnore]
        public ThumbnailSize ThumbnailSize { get; set; }

        /// <summary>
        /// Ширина исходного изображения.
        /// </summary>
        [JsonIgnore]
        public double Width { get { return 640; } }

        /// <summary>
        /// Высота исходного изображения.
        /// </summary>
        [JsonIgnore]
        public double Height { get { return 480; } }

        /// <summary>
        /// Источник изображения миниатюры.
        /// </summary>
        [JsonIgnore]
        public string ThumbnailSource
        {
            get { return ThumbnailSize.Width <= 130 ? Photo130 : Photo320; }
        }

        /// <summary>
        /// Возвращает соотношение ширины к высоте исходного изображения.
        /// </summary>
        public double GetRatio() { return Width / Height; }
        #endregion
    }
}
