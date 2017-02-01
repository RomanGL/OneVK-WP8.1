using Newtonsoft.Json;
using OneVK.Core.Models.Json;
using PropertyChanged;
using System;

namespace OneVK.Core.VK.Models.Common
{
    /// <summary>
    /// Представляет собой место ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKPlace
    {
        /// <summary>
        /// Название места.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Географическая широта, заданная в градусах.
        /// </summary>
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        /// <summary>
        /// Географическая долгота места, заданная в градусах.
        /// </summary>
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        /// <summary>
        /// Тип места.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        /// <summary>
        /// Адрес места.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }
        /// <summary>
        /// Идентификатор места.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }
        /// <summary>
        /// Дата создания.
        /// </summary>
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        /// <summary>
        /// Название страны.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
        /// <summary>
        /// Название города.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }
        /// <summary>
        /// Ссылка на изображение иконки.
        /// </summary>
        [JsonProperty("icon")]
        public string IconUrl { get; set; }
        /// <summary>
        /// Время последнего чекина.
        /// </summary>
        [JsonProperty("updated")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Updated { get; set; }
        /// <summary>
        /// Количество чекинов.
        /// </summary>
        [JsonProperty("checkins")]
        public uint Checkins { get; set; }
        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupID { get; set; }
        /// <summary>
        /// Миниатюра аватара сообщества.
        /// </summary>
        [JsonProperty("group_photo")]
        public string GroupPhoto { get; set; }
    }
}
