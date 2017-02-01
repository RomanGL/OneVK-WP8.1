using Newtonsoft.Json;
using PropertyChanged;
using System.Collections.Generic;
using Microsoft.Practices.Prism.StoreApps;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Представляет собой персональную информацию 
    /// пользователя (из раздела "Жизенная позиция").
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKPersonalInfo : BindableBase
    {
        /// <summary>
        /// Политические взгляды пользователя.
        /// </summary>
        [JsonProperty("political")]
        public VKPolitical Political { get; set; }
        /// <summary>
        /// Главное в людях.
        /// </summary>
        [JsonProperty("people_main")]
        public VKPeopleMain PeopleMain { get; set; }
        /// <summary>
        /// Главное в жизни.
        /// </summary>
        [JsonProperty("life_main")]
        public VKLifeMain LifeMain { get; set; }
        /// <summary>
        /// Отношение к курению.
        /// </summary>
        [JsonProperty("smoking")]
        public VKSmoking Smoking { get; set; }
        /// <summary>
        /// Отношение к алкоголю.
        /// </summary>
        [JsonProperty("alcohol")]
        public VKAlcohol Alcohol { get; set; }
        /// <summary>
        /// Языки, которые знает пользователь.
        /// </summary>
        [JsonProperty("langs")]
        public List<string> Languages { get; set; }
        /// <summary>
        /// Мировоззрение.
        /// </summary>
        [JsonProperty("religion")]
        public string Religion { get; set; }
        /// <summary>
        /// Источники вдохновления.
        /// </summary>
        [JsonProperty("inspired_by")]
        public string InspiredBy { get; set; }
    }
}
