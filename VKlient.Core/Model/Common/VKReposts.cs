using Newtonsoft.Json;
using OneVK.Enums.Common;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Информация о репостах записи.
    /// </summary>
    public sealed class VKReposts
    {
        /// <summary>
        /// Количество репостов записи.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Репостнул ли текущий пользователль запись.
        /// </summary>
        [JsonProperty("user_reposted")]
        public VKBoolean UserReposted { get; set; }
    }
}
