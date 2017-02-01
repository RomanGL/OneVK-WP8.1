using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Представляет собой информацию о роде деятельности пользователя.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKOccupation : BindableBase
    {                
        /// <summary>
        /// Идентификатор школы, вуза или группы компаний
        /// (в которой работает пользователь).
        /// </summary>
        [JsonProperty("id")]
        public uint ID { get; set; }

        /// <summary>
        /// Название школы, выза или места работы.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Тип рода деятельности.
        /// </summary>
        [JsonProperty("type")]
        public VKOccupationType Occupation { get; set; }
    }
}
