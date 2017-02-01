using Newtonsoft.Json;
using System;
using OneVK.Core.Json;

namespace OneVK.Model.Photo
{
    /// <summary>
    /// Представляет собой фотографию ВКонтакте с информацией об 
    /// установленной отметке.
    /// </summary>
    public class VKPhotoWithTag : VKPhoto
    {
        /// <summary>
        /// Идентификатор пользователя, сделавшего отметку.
        /// </summary>
        [JsonProperty("placer_id")]
        public long PlacerID { get; set; }
        /// <summary>
        /// Дата создания отметки.
        /// </summary>
        [JsonProperty("tag_created")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime TagCreated { get; set; }
        /// <summary>
        /// Идентификатор отметки.
        /// </summary>
        [JsonProperty("tag_id")]
        public long TagID { get; set; }
    }
}
