using Newtonsoft.Json;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Информация о высшем образовании пользователя.
    /// </summary>
    [ImplementPropertyChanged]
    public class VKUserEducation : BindableBase
    {
        /// <summary>
        /// Иденетификатор университета.
        /// </summary>
        [JsonProperty("university")]
        public uint ID { get; set; }
        
        /// <summary>
        /// Идентификатор факультета.
        /// </summary>
        [JsonProperty("faculty")]
        public uint FacultyID { get; set; }
        
        /// <summary>
        /// Название университета.
        /// </summary>
        [JsonProperty("university_name")]
        public string UniversityName { get; set; }
        
        /// <summary>
        /// Название факультета.
        /// </summary>
        [JsonProperty("faculty_name")]
        public string FacultyName { get; set; }
        
        /// <summary>
        /// Год окончания.
        /// </summary>
        [JsonProperty("graduation")]
        public ushort Graduation { get; set; }
    }
}
