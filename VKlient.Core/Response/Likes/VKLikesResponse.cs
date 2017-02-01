using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запросы AddLike и DeleteLike.
    /// </summary>
    public class VKLikesResponse
    {
        /// <summary>
        /// Текущее количество отметок "Мне нравится" у объекта.
        /// </summary>
        [JsonProperty("likes")]
        public uint LikesCount { get; set; }
    }
}
