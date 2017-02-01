using Newtonsoft.Json;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой списковый ответ ВКонтакте на запрос.
    /// </summary>
    public class VKCountedItemsObject<T> : VKBaseItemsObject<T>
    {
        /// <summary>
        /// Общее количество элементов.
        /// </summary>
        [JsonProperty("count")]
        public uint Count { get; set; }        
    }
}
