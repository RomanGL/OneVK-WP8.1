using Newtonsoft.Json;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Информация о высшем образовании пользователя.
    /// </summary>
    public class VKUserEducation : IVKObject
    {
        /// <summary>
        /// Иденетификатор университета.
        /// </summary>
        [JsonProperty("university")]
        public long ID { get; set; }
        /// <summary>
        /// Идентификатор факультета.
        /// </summary>
        [JsonProperty("faculty")]
        public int FacultyID { get; set; }
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
        public int Graduation { get; set; }
    }
}
