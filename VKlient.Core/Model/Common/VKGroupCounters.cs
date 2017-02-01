using Newtonsoft.Json;
using System;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Счетчики в группах ВКонтакте.
    /// </summary>
    public sealed class VKGroupCounters : VKCountersBase
    {        
        /// <summary>
        /// Количество топиков в обсуждениях.
        /// </summary>
        [JsonProperty("topics")]
        public uint TopicsCount { get; set; }
        /// <summary>
        /// Количество документов.
        /// </summary>
        [JsonProperty("docs")]
        public uint DocsCount { get; set; }
    }
}
