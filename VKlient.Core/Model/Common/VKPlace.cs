using System;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой место ВКонтакте.
    /// </summary>
    public sealed class VKPlace : IVKObject
    {
        /// <summary>
        /// Название места.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Географическая широта, заданная в градусах.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Географическая долгота места, заданная в градусах.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Тип места.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Идентификатор страны.
        /// </summary>
        public int CountryID { get; set; }
        /// <summary>
        /// Идентификатор города.
        /// </summary>
        public int CityID { get; set; }
        /// <summary>
        /// Адрес места.
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// Идентификатор места.
        /// </summary>
        public long ID { get; set; }
    }
}
