using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using OneVK.Model.Common;
using OneVK.Enums.Common;
using GalaSoft.MvvmLight;

namespace OneVK.Model
{
    /// <summary>
    /// Представляет медиаэлемент Last.fm.
    /// </summary>
    public class LFMediaElement
    {
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
        /// Список изображений элемента.
        /// </summary>
        [JsonProperty("image")]
        public List<LFImage> Images { get; set; }

        /// <summary>
        /// Возвращает среднее изображение.
        /// </summary>
        public LFImage MediumImage
        {
            get
            {
                LFImage img = null;
                if (Images != null && Images.Count != 0)
                    img = Images.FindLast(e => e.Size == LFImageSize.small || e.Size == LFImageSize.medium);

                return img ?? new LFImage { URL = "ms-appx:///Assets/ImagePlaceholder.png" };
            }
        }

        /// <summary>
        /// Возвращает большое изображение.
        /// </summary>
        public LFImage LargeImage
        {
            get
            {
                LFImage img = null;
                if (Images != null && Images.Count != 0)
                    img = Images.FindLast(e => e.Size == LFImageSize.small || e.Size == LFImageSize.medium || e.Size == LFImageSize.large);

                return img ?? new LFImage { URL = "ms-appx:///Assets/ImagePlaceholder.png" };
            }
        }

        /// <summary>
        /// Возвращает очень большое изображение.
        /// </summary>
        public LFImage ExtraLargeImage
        {
            get
            {
                LFImage img = null;
                if (Images != null && Images.Count != 0)
                    img = Images.FindLast(e => e.Size == LFImageSize.small || e.Size == LFImageSize.medium ||
                        e.Size == LFImageSize.large || e.Size == LFImageSize.extralarge);

                return img ?? new LFImage { URL = "ms-appx:///Assets/ImagePlaceholder.png" };
            }
        }

        /// <summary>
        /// Возвращает очень-очень большое изображение.
        /// </summary>
        public LFImage MegaImage
        {
            get
            {
                LFImage img = null;
                if (Images != null && Images.Count != 0)
                    img = Images.FindLast(e => e.Size == LFImageSize.small || e.Size == LFImageSize.medium ||
                        e.Size == LFImageSize.large || e.Size == LFImageSize.extralarge || e.Size == LFImageSize.mega);

                return img ?? new LFImage { URL = "ms-appx:///Assets/ImagePlaceholder.png" };
            }
        }
    }
}
