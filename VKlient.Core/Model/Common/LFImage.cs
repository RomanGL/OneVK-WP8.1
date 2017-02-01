using Newtonsoft.Json;
using OneVK.Enums.Common;
using Newtonsoft.Json.Converters;
using OneVK.Core.Json;

namespace OneVK.Model.Common
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
