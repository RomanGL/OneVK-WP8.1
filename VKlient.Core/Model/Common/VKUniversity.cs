using Newtonsoft.Json;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой объект университета ВКонтакте.
    /// </summary>
    public sealed class VKUniversity : IVKObject
    {
        /// <summary>
        /// Идентификатор университета.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
        /// <summary>
        /// Название университета.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Идентификатор страны, в которой находится университет.
        /// </summary>
        [JsonProperty("country")]
        public int CountryID { get; set; }
        /// <summary>
        /// Идентификатор города, в котором находится университет.
        /// </summary>
        [JsonProperty("city")]
        public int CityID { get; set; }
        /// <summary>
        /// Идентификатор факультета.
        /// </summary>
        [JsonProperty("faculty")]
        public int FacultyID { get; set; }
        /// <summary>
        /// Название факультета.
        /// </summary>
        [JsonProperty("faculty_name")]
        public string FacultyName { get; set; }
        /// <summary>
        /// Идентификатор кафедры.
        /// </summary>
        [JsonProperty("chair")]
        public int ChairID { get; set; }
        /// <summary>
        /// Название кафедры.
        /// </summary>
        [JsonProperty("chair_name")]
        public string ChairName { get; set; }
        /// <summary>
        /// Год окончания обучения.
        /// </summary>
        [JsonProperty("graduation")]
        public int Graduation { get; set; }
    }
}
