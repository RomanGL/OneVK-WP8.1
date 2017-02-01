using Newtonsoft.Json;
using OneVK.Core.Models.Json;
using OneVK.Core.VK.Json;
using OneVK.Core.VK.Models.Common;
using PropertyChanged;
using System;
using System.Collections.Generic;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Представляет расширенное сообщество ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public class VKGroupExtended : VKGroup
    {
        [DoNotNotify]
        [JsonProperty("start_date")]
        private long startDate { get; set; }

        /// <summary>
        /// Информация о блокировке пользователя.
        /// </summary>
        [JsonProperty("ban_info")]
        public VKUserBanInfo BanInfo { get; set; }
        /// <summary>
        /// Город сообщества.
        /// </summary>
        [JsonProperty("city")]
        public VKCity City { get; set; }
        /// <summary>
        /// Страна сообщества.
        /// </summary>
        [JsonProperty("country")]
        public VKCountry Country { get; set; }
        /// <summary>
        /// Место, указанное в сообществе.
        /// </summary>
        [JsonProperty("place")]
        public VKPlace Place { get; set; }
        /// <summary>
        /// Описание сообщества.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Название главной вики-страницы сообщества.
        /// </summary>
        [JsonProperty("wiki_page")]
        public string WikiPage { get; set; }
        /// <summary>
        /// Количество участников сообщества.
        /// </summary>
        [JsonProperty("members_count")]
        public ulong MembersCount { get; set; }
        /// <summary>
        /// Счетчики сообщества.
        /// </summary>
        [JsonConverter(typeof(VKGroupCountersConverter))]
        [JsonProperty("counters")]
        public VKGroupCounters Counters { get; set; }
        /// <summary>
        /// Дата начала встречи/дата основания.
        /// </summary>      
        [DoNotNotify]
        public DateTime StartDate
        {
            get
            {
                if (startDate == 0) return DateTime.MinValue;
                if (FinishDate.Ticks != 0) return UnixtimeToDateTimeConverter.GetDateTimeFromUnixTime(startDate);
                else
                {
                    int input = (int)startDate;
                    int day = input % 100;
                    input = input / 100;
                    int month = input % 100;
                    return new DateTime(input / 100, month, day);
                }
            }
        }
        /// <summary>
        /// Дата окончания.
        /// </summary>
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        [JsonProperty("finish_date")]
        public DateTime FinishDate { get; set; }
        /// <summary>
        /// Текст описания для публичных страниц.
        /// </summary>
        [JsonProperty("public_date_label")]
        public string PublicDateLabel { get; set; }
        /// <summary>
        /// Может ли пользователь размещать записи.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("can_post")]
        public bool CanPost { get; set; }
        /// <summary>
        /// Может ли пользователь видеть чужие записи.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("can_see_all_posts")]
        public bool CanSeeAllPosts { get; set; }
        /// <summary>
        /// Может ли пользователь загружать документы.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("can_upload_doc")]
        public bool CanUploadDoc { get; set; }
        /// <summary>
        /// Может ли пользователь загружать видео.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("can_upload_video")]
        public bool CanUploadVideo { get; set; }
        /// <summary>
        /// Может ли пользователь создавать темы в обсуждениях.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("can_create_topic")]
        public bool CanCreateTopic { get; set; }
        /// <summary>
        /// Строка состояния публичной страницы.
        /// </summary>
        [JsonProperty("activity")]
        public string Activity { get; set; }
        /// <summary>
        /// Статус сообщества.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// Контакты сообщества.
        /// </summary>
        [JsonProperty("contacts")]
        public List<VKContact> Contacts { get; set; }
        /// <summary>
        /// Ссылки сообщества.
        /// </summary>
        [JsonProperty("links")]
        public List<VKLink> Links { get; set; }
        /// <summary>
        /// Идентификатор закрепленного поста.
        /// </summary>
        [JsonProperty("fixed_post")]
        public long FixedPostID { get; set; }
        /// <summary>
        /// Верифицировано ли сообщество.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("verified")]
        public bool IsVerified { get; set; }
        /// <summary>
        /// Сайт сообщества.
        /// </summary>
        [JsonProperty("site")]
        public string Site { get; set; }
        /// <summary>
        /// Идентификатор основного альбома сообщества.
        /// </summary>
        [JsonProperty("main_album_id")]
        public uint MainAlbumID { get; set; }
        /// <summary>
        /// Находится ли сообщество в закладках у пользователя.
        /// </summary>
        [JsonProperty("is_favorite")]
        public bool IsFavorite { get; set; }
        /// <summary>
        /// Скрыты ли новости от сообщества в ленте пользователя.
        /// </summary>
        [JsonProperty("is_hidded_from_feed")]
        public bool IsHiddenFromFeed { get; set; }
        /// <summary>
        /// Главная секция в сообществе.
        /// </summary>
        [JsonProperty("main_section")]
        public VKGroupMainSection MainSection { get; set; }
    }
}
