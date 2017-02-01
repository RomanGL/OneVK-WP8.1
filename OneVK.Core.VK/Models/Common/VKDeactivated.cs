using Newtonsoft.Json;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Информацио том, деактивирована ли страница.
    /// </summary>
    public enum VKDeactivated : byte
    {
        /// <summary>
        /// Страница не деактивирована.
        /// </summary>
        [JsonProperty("not")]
        NotDeactivated = 0,
        /// <summary>
        /// Страница удалена.
        /// </summary>
        [JsonProperty("deleted")]
        Deleted,
        /// <summary>
        /// Страница забанена.
        /// </summary>
        [JsonProperty("banned")]
        Banned
    }
}
