using Newtonsoft.Json;
using System;
using OneVK.Core.Json;
using OneVK.Enums.Common;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой отметку пользователя ВКонтакте.
    /// </summary>
    public class VKTag : IVKObject
    {
        /// <summary>
        /// Идентификатор пользователя, которому соответствует отметка.
        /// </summary>
        [JsonProperty("user_id")]
        public long UserID { get; set; }

        /// <summary>
        /// Идентификатор отметки.
        /// </summary>
        [JsonProperty("tag_id")]
        public long ID { get; set; }

        /// <summary>
        /// Идентификатор пользователя, сделавшего отметку.
        /// </summary>
        [JsonProperty("placer_id")]
        public long PlacerID { get; set; }

        /// <summary>
        /// Название отметки.
        /// </summary>
        [JsonProperty("tagged_name")]
        public string TaggedName { get; set; }

        /// <summary>
        /// Дата добавления отметки в формате unixtime.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Статус отметки.
        /// </summary>
        [JsonProperty("viewed")]
        public VKBoolean Viewed { get; set; }
    }
}
