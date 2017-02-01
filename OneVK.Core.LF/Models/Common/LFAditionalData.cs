using Newtonsoft.Json;

namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Представляет дополнительную информацию об ответе Last.fm, 
    /// такую как общее количество результатов, текущую страницу 
    /// и их общее количество.
    /// </summary>
    public class LFAditionalData
    {
        /// <summary>
        /// Текущая страница.
        /// </summary>
        [JsonProperty("page")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Количество элементов на странице.
        /// </summary>
        [JsonProperty("perPage")]
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Общее количество страниц.
        /// </summary>
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Общее количество элементов.
        /// </summary>
        [JsonProperty("total")]
        public int TotalItems { get; set; }
    }
}
