using Newtonsoft.Json;
using OneVK.Enums.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос отметки "Мне нравится" 
    /// к объекту от указанного пользователя.
    /// </summary>
    public class VKIsLikedObject
    {
        /// <summary>
        /// Установлена ли отметка мне нравится.
        /// </summary>
        [JsonProperty("liked")]
        public VKBoolean Likes { get; set; }

        /// <summary>
        /// Репостнул ли пользователь этот объект.
        /// </summary>
        [JsonProperty("copied")]
        public VKBoolean Reposted { get; set; }
    }
}
