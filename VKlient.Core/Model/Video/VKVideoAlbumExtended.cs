using Newtonsoft.Json;
using System;
using OneVK.Core.Json;

namespace OneVK.Model.Video
{
    /// <summary>
    /// Представляет собой альбом видеозаписей ВКонтакте с расширенной информацией.
    /// </summary>
    public class VKVideoAlbumExtended : VKVideoAlbum
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("photo_320")]
        public string Photo320 { get; set; }
        
        [JsonProperty("photo_160")]
        public string Photo160 { get; set; }

        [JsonProperty("updated_time")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime UpdatedTime { get; set; }
    }
}
