using Newtonsoft.Json;
using OneVK.Core.LF.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет изображение Last.fm.
    /// </summary>
    public class LFImage
    {
        /// <summary>
        /// Ссылка на изображение.
        /// </summary>
        [JsonProperty("#text")]
        public string URL { get; set; }

        /// <summary>
        /// Размер изображения.
        /// </summary>
        [JsonConverter(typeof(LFImageSizeConverter))]
        [JsonProperty("size")]
        public LFImageSize Size { get; set; }
    }
}
