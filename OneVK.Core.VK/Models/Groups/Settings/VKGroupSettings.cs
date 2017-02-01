using Microsoft.Practices.Prism.StoreApps;
using Newtonsoft.Json;
using OneVK.Core.VK.Models.Common;
using PropertyChanged;
using System.Collections.Generic;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Представляет настройки сообщества.
    /// </summary>
    [ImplementPropertyChanged]
    public sealed class VKGroupSettings : BindableBase
    {
        /// <summary>
        /// Название сообщества.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        /// <summary>
        /// Описание сообщества.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Короткий адрес страницы.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }
        /// <summary>
        /// Электронный адрес сообщества.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// Номер телефона сообщества.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Сайт сообщества.
        /// </summary>
        [JsonProperty("website")]
        public string Site { get; set; }
        /// <summary>
        /// Местоположение сообщества.
        /// </summary>
        [JsonProperty("place")]
        public VKPlace Place { get; set; }
        /// <summary>
        /// Тип доступа к сообществу.
        /// </summary>
        [JsonProperty("access")]
        public VKGroupAccess Access { get; set; }
        /// <summary>
        /// Режим работы стены.
        /// </summary>
        [JsonProperty("wall")]
        public VKFourStateModule Wall { get; set; }
        /// <summary>
        /// Режим работы обсуждений.
        /// </summary>
        [JsonProperty("topics")]
        public VKThreeStateModule Topics { get; set; }
        /// <summary>
        /// Режим работы фотографий.
        /// </summary>
        [JsonProperty("photos")]
        public VKThreeStateModule Photos { get; set; }
        /// <summary>
        /// Режим работы видеозаписей.
        /// </summary>
        [JsonProperty("video")]
        public VKThreeStateModule Videos { get; set; }
        /// <summary>
        /// Режим работы видеозаписей.
        /// </summary>
        [JsonProperty("audio")]
        public VKThreeStateModule Audios { get; set; }
        /// <summary>
        /// Режим работы блока ссылок (только для публичных страниц).
        /// </summary>
        [JsonProperty("links")]
        public VKTwoStateModule Links { get; set; }
        /// <summary>
        /// Режим работы событий (только для публичных страниц).
        /// </summary>
        [JsonProperty("events")]
        public VKTwoStateModule Events { get; set; }
        /// <summary>
        /// Режим работы мест (только для публичных страниц).
        /// </summary>
        [JsonProperty("places")]
        public VKTwoStateModule Places { get; set; }
        /// <summary>
        /// Режим работы контактов (только для публичных страниц).
        /// </summary>
        [JsonProperty("contacts")]
        public VKTwoStateModule Contacts { get; set; }
        /// <summary>
        /// Режим работы документов.
        /// </summary>
        [JsonProperty("docs")]
        public VKThreeStateModule Docs { get; set; }
        /// <summary>
        /// Режим работы вики-страниц.
        /// </summary>
        [JsonProperty("wiki")]
        public VKThreeStateModule Wiki { get; set; }
        /// <summary>
        /// Индекс элемента тематики сообщества.
        /// </summary>
        [JsonProperty("subject")]
        public uint Subject { get; set; }
        /// <summary>
        /// Доступные варианты тематики сообщества.
        /// </summary>
        [JsonProperty("subject_list")]
        public List<VKGroupSubject> AvailableSubjects { get; set; }
    }
}
