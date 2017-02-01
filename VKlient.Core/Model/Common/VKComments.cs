using Newtonsoft.Json;
using OneVK.Enums.Common;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Информация о комментариях к записи.
    /// </summary>
    public sealed class VKComments
    {
        /// <summary>
        /// Количество комментариев к записи.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
        /// <summary>
        /// Может ли текущий пользователь оставить 
        /// комментарий под записью.
        /// </summary>
        [JsonProperty("can_post")]
        public VKBoolean CanComment { get; set; }
    }
}
