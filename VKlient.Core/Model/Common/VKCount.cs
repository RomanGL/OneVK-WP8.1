using Newtonsoft.Json;
using System;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой количество определенных объектов в текущем объекте.
    /// </summary>
    public sealed class VKCount
    {
        /// <summary>
        /// Количество элементов.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
