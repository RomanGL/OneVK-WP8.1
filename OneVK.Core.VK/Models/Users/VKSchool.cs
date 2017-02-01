using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Представляет собой школу ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]    
    public sealed class VKSchool : BindableBase
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
        /// Идентификатор типа школы.
        /// </summary>
        [JsonProperty("type")]
        public uint TypeID { get; set; }
        
        /// <summary>
        /// Название типа школы.
        /// </summary>
        [JsonProperty("type_str")]
        public string TypeName { get; set; }
        
        /// <summary>
        /// Год начала обучения.
        /// </summary>
        [JsonProperty("year_from")]
        public ushort YearFrom { get; set; }
        
        /// <summary>
        /// Год окончания обучения.
        /// </summary>
        [JsonProperty("year_to")]
        public ushort YearTo { get; set; }
        
        /// <summary>
        /// Год выпуска.
        /// </summary>
        [JsonProperty("year_graduated")]
        public ushort YearGraduated { get; set; }
        
        /// <summary>
        /// Буква класса.
        /// </summary>
        [JsonProperty("class")]
        public string ClassLetter { get; set; }
        
        /// <summary>
        /// Специализация.
        /// </summary>
        [JsonProperty("speciality")]
        public string Speciality { get; set; }
    }
}
