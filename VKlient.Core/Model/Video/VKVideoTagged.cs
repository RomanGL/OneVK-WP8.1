using Newtonsoft.Json;
using System;
using OneVK.Core.Json;

namespace OneVK.Model.Video
{
    public class VKVideoTagged : VKVideoBase
    {
        /// <summary>
        /// Идентификатор пользователя, сделавшего отметку.
        /// </summary>
        [JsonProperty("placer_id")]
        public ulong PlacerID { get; set; }
        /// <summary>
        /// Дата создания отметки в формате unixtime.
        /// </summary>
        [JsonProperty("tag_created")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime TagCreated { get; set; }
        /// <summary>
        /// Идентификатор отметки.
        /// </summary>
        [JsonProperty("tag_id")]
        public ulong TagID { get; set; }
    }
}
