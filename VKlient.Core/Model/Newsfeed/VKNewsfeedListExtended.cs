using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Enums.Common;

namespace OneVK.Model.Newsfeed
{
    /// <summary>
    /// Объект пользовательского списка с расширенной информацией.
    /// </summary>
    public sealed class VKNewsfeedListExtended : VKNewsfeedList
    {
        /// <summary>
        /// Возвращать ли записи, которые являются копиями записей с чужой стены.
        /// </summary>
        [JsonProperty("no_reposts")]
        public VKBoolean NoReposts { get; set; }

        /// <summary>
        /// Идентификаторы источников, включенных в список.
        /// </summary>
        [JsonProperty("source_ids")]
        public List<long> SourceIDs { get; set; }
    }
}
