using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Представляет собой объект университета ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKUniversity : BindableBase
    {
        /// <summary>
        /// Идентификатор университета.
        /// </summary>
        [JsonProperty("id")]
        public uint ID { get; set; }
        
        /// <summary>
        /// Название университета.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Идентификатор страны, в которой находится университет.
        /// </summary>
        [JsonProperty("country")]
        public uint CountryID { get; set; }
        
        /// <summary>
        /// Идентификатор города, в котором находится университет.
        /// </summary>
        [JsonProperty("city")]
        public uint CityID { get; set; }
        
        /// <summary>
        /// Идентификатор факультета.
        /// </summary>
        [JsonProperty("faculty")]
        public uint FacultyID { get; set; }
        
        /// <summary>
        /// Название факультета.
        /// </summary>
        [JsonProperty("faculty_name")]
        public string FacultyName { get; set; }
        
        /// <summary>
        /// Идентификатор кафедры.
        /// </summary>
        [JsonProperty("chair")]
        public uint ChairID { get; set; }
        
        /// <summary>
        /// Название кафедры.
        /// </summary>
        [JsonProperty("chair_name")]
        public string ChairName { get; set; }
        
        /// <summary>
        /// Год окончания обучения.
        /// </summary>
        [JsonProperty("graduation")]
        public ushort Graduation { get; set; }
    }
}
