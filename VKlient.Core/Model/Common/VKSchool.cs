using Newtonsoft.Json;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой школу ВКонтакте.
    /// </summary>
    public sealed class VKSchool : IVKObject
    {
        /// <summary>
        /// Идентификатор школы.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
        /// <summary>
        /// Название школы.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Идентификатор страны, в которой находится школа.
        /// </summary>
        [JsonProperty("country")]
        public int CountryID { get; set; }
        /// <summary>
        /// Идентификатор города, в котором находится школа.
        /// </summary>
        [JsonProperty("city")]
        public int CityID { get; set; }
        /// <summary>
        /// Идентификатор типа школы.
        /// </summary>
        [JsonProperty("type")]
        public int TypeID { get; set; }
        /// <summary>
        /// Название типа школы.
        /// </summary>
        [JsonProperty("type_str")]
        public string TypeName { get; set; }
        /// <summary>
        /// Год начала обучения.
        /// </summary>
        [JsonProperty("year_from")]
        public int YearFrom { get; set; }
        /// <summary>
        /// Год окончания обучения.
        /// </summary>
        [JsonProperty("year_to")]
        public int YearTo { get; set; }
        /// <summary>
        /// Год выпуска.
        /// </summary>
        [JsonProperty("year_graduated")]
        public int YearGraduated { get; set; }
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
