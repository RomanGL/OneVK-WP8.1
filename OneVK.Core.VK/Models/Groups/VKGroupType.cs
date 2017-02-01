using Newtonsoft.Json;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Тип собщества ВКонтакте.
    /// </summary>
    public enum VKGroupType : byte
    {
        /// <summary>
        /// Группа.
        /// </summary>
        [JsonProperty("group")]
        Group = 0,
        /// <summary>
        /// Публичная страница.
        /// </summary>
        [JsonProperty("page")]
        Page,
        /// <summary>
        /// Мероприятие.
        /// </summary>
        [JsonProperty("event")]
        Event
    }
}
