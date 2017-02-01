using Newtonsoft.Json;
using System;
#if ONEVK_CORE
using Windows.Foundation;
#endif

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой информацию о местоположении ВКонтакте.
    /// </summary>
    public sealed class VKGeo
    {
        /// <summary>
        /// Тип места.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Координаты места.
        /// </summary>
        //[JsonProperty("coordinates")]
        //public Point Coordinates { get; set; }
        /// <summary>
        /// Описание места (если добавлено).
        /// </summary>
        [JsonProperty("place")]
        public VKPlace Place { get; set; }
    }
}
