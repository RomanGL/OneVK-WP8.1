using System;
using Newtonsoft.Json;
using OneVK.Core;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой стикер ВКонтакте.
    /// </summary>
    public sealed class VKSticker : IThumbnailSupport
    {
        /// <summary>
        /// Идентификатор стикера.
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        [JsonProperty("product_id")]
        public int ProductID { get; set; }

        /// <summary>
        /// Картинка стикера высотой 64 пиксела.
        /// </summary>
        [JsonProperty("photo_64")]
        public string Photo64 { get; set; }

        /// <summary>
        /// Картинка стикера высотой 128 пиксела.
        /// </summary>
        [JsonProperty("photo_128")]
        public string Photo128 { get; set; }

        /// <summary>
        /// Картинка стикера высотой 256 пиксела.
        /// </summary>
        [JsonProperty("photo_256")]
        public string Photo256 { get; set; }

        /// <summary>
        /// Картинка стикера высотой 352 пиксела.
        /// </summary>
        [JsonProperty("photo_352")]
        public string Photo352 { get; set; }

        /// <summary>
        /// Картинка стикера высотой 512 пиксела.
        /// </summary>
        [JsonProperty("photo_512")]
        public string Photo512 { get; set; }

        /// <summary>
        /// Ширина стикера.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Высота стикера.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        #region IThumbnailSupport
        /// <summary>
        /// Данные для визуализации миниатюры.
        /// </summary>
        public ThumbnailSize ThumbnailSize { get; set; }
        /// <summary>
        /// Ширнина исходного изображения.
        /// </summary>
        double IThumbnailSupport.Width { get { return Width; } }
        /// <summary>
        /// Высота исходного изображения.
        /// </summary>
        double IThumbnailSupport.Height { get { return Height; } }
        /// <summary>
        /// Источник изображения миниатюры.
        /// </summary>
        public string ThumbnailSource { get { return Photo256; } }

        /// <summary>
        /// Возвращает соотношение ширины к высоте исходного изображения.
        /// </summary>
        public double GetRatio() { return (double)Width / (double)Height; }
        #endregion
    }
}
