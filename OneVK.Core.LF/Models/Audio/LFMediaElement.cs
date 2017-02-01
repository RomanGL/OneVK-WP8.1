using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace OneVK.Core.LF.Models
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
                    img = Images.FindLast(e => e.Size == LFImageSize.Small || e.Size == LFImageSize.Medium);

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
                    img = Images.FindLast(e => e.Size == LFImageSize.Small || e.Size == LFImageSize.Medium || e.Size == LFImageSize.Large);

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
                    img = Images.FindLast(e => e.Size == LFImageSize.Small || e.Size == LFImageSize.Medium ||
                        e.Size == LFImageSize.Large || e.Size == LFImageSize.ExtraLarge);

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
                    img = Images.FindLast(e => e.Size == LFImageSize.Small || e.Size == LFImageSize.Medium ||
                        e.Size == LFImageSize.Large || e.Size == LFImageSize.ExtraLarge || e.Size == LFImageSize.Mega);

                return img ?? new LFImage { URL = "ms-appx:///Assets/ImagePlaceholder.png" };
            }
        }
    }
}
