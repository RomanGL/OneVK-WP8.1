using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Enums.Common;
using OneVK.Model.Common;
using System;
using System.Collections.Generic;

namespace OneVK.Model.Group
{
    /// <summary>
    /// Сообщество ВКонтакте с расширенными данными.
    /// </summary>
    public sealed class VKGroupExtended : VKGroupBase
    {
        [JsonProperty("start_date")]
        private long startDate { get; set; }

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
        /// Место, указанное в информации о сообществе.
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
        public long MembersCount { get; set; }
        /// <summary>
        /// Счетчики сообщества.
        /// </summary>
        [JsonProperty("counters", NullValueHandling = NullValueHandling.Ignore)]
        public VKGroupCounters Counters { get; set; }
        /// <summary>
        /// Дата начала встречи/дата основания.
        /// </summary>        
        public DateTime StartDate
        {
            get
            {
                if (startDate == 0) return new DateTime();
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
        /// Дата окончания встречи.
        /// </summary>
        [JsonProperty("finish_date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime FinishDate { get; set; }
        /// <summary>
        /// Может ли текущий пользователь оставлять
        /// записи на стене сообщества.
        /// </summary>
        [JsonProperty("can_post")]
        public VKBoolean CanPost { get; set; }
        /// <summary>
        /// Может ли текущий пользователь видеть чужие
        /// записи на стене сообщества.
        /// </summary>
        [JsonProperty("can_see_all_posts")]
        public VKBoolean CanSeeAllPosts { get; set; }
        /// <summary>
        /// Может ли текущий пользователь загружать 
        /// документы в сообщество.
        /// </summary>
        [JsonProperty("can_upload_doc")]
        public VKBoolean CanUploadDoc { get; set; }
        /// <summary>
        /// Может ли текущий пользователь загружать
        /// видеозаписи в сообщество.
        /// </summary>
        [JsonProperty("can_upload_video")]
        public VKBoolean CanUploadVideo { get; set; }
        /// <summary>
        /// Может ли пользователь создавать темы в обсуждениях.
        /// </summary>
        [JsonProperty("can_create_topic")]
        public VKBoolean CanCreateTopic { get; set; }
        /// <summary>
        /// Строка состояния публичной страницы/открыта группа
        /// или нет/дата начала у событий.
        /// </summary>
        [JsonProperty("activity")]
        public string Activity { get; set; }
        /// <summary>
        /// Статус сообщества.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// Список контактов сообщества.
        /// </summary>
        [JsonProperty("contacts")]
        public List<VKContact> Contacts { get; set; }
        /// <summary>
        /// Список ссылок сообщества.
        /// </summary>
        [JsonProperty("links")]
        public List<VKLink> Links { get; set; }
        /// <summary>
        /// Идентификатор закрепленного поста в сообществе.
        /// </summary>
        [JsonProperty("fixed_post_id")]
        public long FixedPostID { get; set; }
        /// <summary>
        /// Является ли сообщество верифицированным.
        /// </summary>
        [JsonProperty("verified")]
        public VKIsVerified Verified { get; set; }
        /// <summary>
        /// Адрес сайта сообщества.
        /// </summary>
        [JsonProperty("site")]
        public string Site { get; set; }

        /// <summary>
        /// Возвращает город и страну сообщества.
        /// </summary>
        public string FullPlace
        {
            get
            {
                if (City != null && Country != null)
                    return string.Format("{0}, {1}", City.Title, Country.Title);
                if (City != null)
                    return City.Title;
                if (Country != null)
                    return Country.Title;
                return string.Empty;
            }
        }

        /// <summary>
        /// Имеется ли у сообщества описание.
        /// </summary>
        public bool HasDescription { get { return !string.IsNullOrEmpty(Description); } }

        /// <summary>
        /// Имеется ли у сообщества местоположение.
        /// </summary>
        public bool HasFullPlace { get { return !string.IsNullOrEmpty(FullPlace); } }

        /// <summary>
        /// Имеется ли у сообщества статус.
        /// </summary>
        public bool HasStatus { get { return !string.IsNullOrEmpty(Status); } }

        /// <summary>
        /// Имеется ли у сообщества местоположение.
        /// </summary>
        public bool HasStartDate { get { return StartDate.Ticks != 0; } }
    }
}
