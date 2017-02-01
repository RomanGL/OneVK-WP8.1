using Newtonsoft.Json;
using System;
using OneVK.Core.Json;
using OneVK.Core;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Helpers;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Фотография из альбома ВКонтакте.
    /// </summary>
    public class VKPhoto : ObservableObject, IVKPhoto, IThumbnailSupport, IVKLikeTarget
    {
        /// <summary>
        /// Идентификатор альбома с фотографией.
        /// </summary>
        [JsonProperty("album_id")]
        public long AlbumID { get; set; }
        /// <summary>
        /// Идентификатор владельца фотографии.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }
        /// <summary>
        /// Идентификатор пользователя, загрузившего фотографию.
        /// Равен 100, если загружена от имени сообщества.
        /// </summary>
        [JsonProperty("user_id")]
        public long UserID { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 75x75px.
        /// </summary>
        [JsonProperty("photo_75")]
        public string Photo75 { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 130x130px. 
        /// </summary>
        [JsonProperty("photo_130")]
        public string Photo130 { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 604x604px. 
        /// </summary>
        [JsonProperty("photo_604")]
        public string Photo604 { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 807x807px. 
        /// </summary>
        [JsonProperty("photo_807")]
        public string Photo807 { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 1280x1024px.
        /// </summary>
        [JsonProperty("photo_1280")]
        public string Photo1280 { get; set; }
        /// <summary>
        /// Ссылка на копию фотографии с максимальным размером 2560x2048px. 
        /// </summary>
        [JsonProperty("photo_2560")]
        public string Photo2560 { get; set; }
        /// <summary>
        /// Ширина оригинала фотографии в пикселах.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
        /// <summary>
        /// Высота оригинала фотографии в пикселах.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
        /// <summary>
        /// Описание фотографии.
        /// </summary>
        [JsonProperty("text")]
        public string Description { get; set; }
        /// <summary>
        /// Дата добавлени фотографии.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }
        /// <summary>
        /// Идентификатор фотографии.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }

        /// <summary>
        /// Ссылка на максимальный средний размер фотографии.
        /// </summary>
        [JsonIgnore]
        public string MediumPhoto { get { return Photo604 ?? Photo130; } }

        #region IThumbnailSupport
        /// <summary>
        /// Данные для визуализации миниатюры.
        /// </summary>
        [JsonIgnore]
        public ThumbnailSize ThumbnailSize { get; set; }

        /// <summary>
        /// Ширнина исходного изображения.
        /// </summary>
        [JsonIgnore]
        double IThumbnailSupport.Width { get { return Width; } }

        /// <summary>
        /// Высота исходного изображения.
        /// </summary>
        [JsonIgnore]
        double IThumbnailSupport.Height { get { return Height; } }

        /// <summary>
        /// Источник изображения миниатюры.
        /// </summary>
        [JsonIgnore]
        public string ThumbnailSource
        {
            get
            {
                var service = ServiceHelper.SettingsService;
                if (service.MaxPhotosSize == VKPhotoSizes.Photo75)
                    return Photo75;
                else if (service.MaxPhotosSize == VKPhotoSizes.Photo130)
                    return Photo130;
                else
                return ThumbnailSize.Width <= 130 ? Photo130 : Photo604;
            }
        }

        /// <summary>
        /// Возвращает соотношение ширины к высоте исходного изображения.
        /// </summary>
        public double GetRatio() { return (double)Width / (double)Height; }
        #endregion

        #region For ImageView

        private VKPhotoSizes? _currentSourceSize;

        /// <summary>
        /// Текущий источник для изображения.
        /// </summary>
        [JsonIgnore]
        public string CurrentSource
        {
            get
            {
                if ((byte)CurrentSourceSize == 6 && !String.IsNullOrEmpty(Photo2560)) return Photo2560;
                if ((byte)CurrentSourceSize >= 5 && !String.IsNullOrEmpty(Photo1280)) return Photo1280;
                if ((byte)CurrentSourceSize >= 4 && !String.IsNullOrEmpty(Photo807)) return Photo807;
                if ((byte)CurrentSourceSize >= 3 && !String.IsNullOrEmpty(Photo604)) return Photo604;
                if ((byte)CurrentSourceSize >= 2 && !String.IsNullOrEmpty(Photo130)) return Photo130;
                return Photo75;
            }
        }

        /// <summary>
        /// Текущий размер источника изображения.
        /// </summary>
        [JsonIgnore]
        public VKPhotoSizes CurrentSourceSize
        {
            get
            {
                if (_currentSourceSize == null)
                    _currentSourceSize = ServiceHelper.SettingsService.MaxPhotosSize;
                return _currentSourceSize.Value;
            }
            set
            {
                Set("CurrentSourceSize", ref _currentSourceSize, value);
                RaisePropertyChanged(() => CurrentSource);
            }
        }

        /// <summary>
        /// Тип элемента для отметки "Мне нравится".
        /// </summary>
        [JsonIgnore]
        public VKLikesTargetType TargetType { get { return VKLikesTargetType.photo; } }

        /// <summary>
        /// Идентификатор владельца элемента.
        /// </summary>
        [JsonIgnore]
        public long LikesOwner { get { return OwnerID; } }

        /// <summary>
        /// Идентификатор элемента.
        /// </summary>
        [JsonIgnore]
        public ulong LikesItem { get { return ID; } }

        /// <summary>
        /// Ключ доступа.
        /// </summary>
        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        #endregion
    }
}
