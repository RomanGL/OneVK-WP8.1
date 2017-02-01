using Newtonsoft.Json;
using OneVK.Enums.Wall;

namespace OneVK.Model.Wall
{
    /// <summary>
    /// Представляет запись, находящуюся в истории 
    /// копирования другой записи.
    /// </summary>
    public class VKAttachedPost : VKWallPost
    {
        /// <summary>
        /// Тип записи.
        /// </summary>
        [JsonProperty("post_type")]
        public VKAttachedPostType Type { get; set; }
    }
}
