using Newtonsoft.Json;
using OneVK.Enums.Profile;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Представляет собой информацию о роде деятельности
    /// пользователя.
    /// </summary>
    public sealed class VKOccupation : IVKObject
    {
        //private const string _work = "work";
        //private const string _school = "school";
        //private const string _university = "university";

        /// <summary>
        /// Тип рода деятельности в виде строки.
        /// </summary>
        //[JsonProperty("type")]
        private string _type { get; set; }
                
        /// <summary>
        /// Идентификатор школы, вуза или группы компаний
        /// (в которой работает пользователь).
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

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
        //{
        //    get
        //    {
        //        switch (_type)
        //        {
        //            case _work:
        //                return VKOccupationType.Work;
        //            case _school:
        //                return VKOccupationType.School;
        //            case _university:
        //                return VKOccupationType.University;
        //            default:
        //                return VKOccupationType.Unknown;
        //        }
        //    }
        //    set
        //    {
        //        switch (value)
        //        {
        //            case VKOccupationType.Unknown:
        //                _type = "";
        //                break;
        //            case VKOccupationType.Work:
        //                _type = _work;
        //                break;
        //            case VKOccupationType.School:
        //                _type = _school;
        //                break;
        //            case VKOccupationType.University:
        //                _type = _university;
        //                break;
        //        }
        //    }
        //}
    }
}
