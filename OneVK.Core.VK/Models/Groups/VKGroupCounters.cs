using OneVK.Core.VK.Models.Common;
using PropertyChanged;
using Newtonsoft.Json;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Представляет счетчики сообщества.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKGroupCounters : VKCountersBase
    {
        /// <summary>
        /// Количество топиков в обсуждении.
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
