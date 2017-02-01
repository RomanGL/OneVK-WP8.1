using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Model.Common;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Профиль пользователя ВКонтакте с расширенной информацией.
    /// </summary>
    public class VKProfileExtended : VKProfileBase
    {
        [JsonProperty("bdate")]
        private string _birthdayDate { get; set; }

        /// <summary>
        /// Находится ли пользователь в черном списке.
        /// </summary>
        [JsonProperty("blacklisted")]
        public VKUserBlacklisted Blacklisted { get; set; }
        /// <summary>
        /// Подверждена ли страница пользователя.
        /// </summary>
        [JsonProperty("verified")]
        public VKIsVerified Verified { get; set; }
        /// <summary>
        /// Доступен ли номер мобильного телефона.
        /// </summary>
        [JsonProperty("has_mobile")]
        public VKUserHasMobile HasMobile { get; set; }
        /// <summary>
        /// Дата дня рождения пользователя.
        /// </summary>  
        public DateTime BirthdayDate
        {
            get { return DateTime.Parse(_birthdayDate); }
            set { _birthdayDate = value.ToString("dd.MM.yyy"); }
        }
        /// <summary>
        /// Фотография пользователя шириной 200 пикс.
        /// </summary>
        [JsonProperty("photo_200_orig")]
        public string PhotoOrig200 { get; set; }
        /// <summary>
        /// Фотография пользователя шириной 400 пикс.
        /// </summary>
        [JsonProperty("photo_400_orig")]
        public string PhotoOrig400 { get; set; }
        /// <summary>
        /// Квадратная фотография пользователя максимального размера.
        /// </summary>
        [JsonProperty("photo_max_orig")]
        public string PhotoOrigMax { get; set; }
        /// <summary>
        /// Фотография пользователя максимального размера.
        /// </summary>
        [JsonProperty("photo_max")]
        public string PhotoMax { get; set; }
        /// <summary>
        /// Родной город пользователя.
        /// </summary>
        [JsonProperty("home_town")]
        public string HomeTown { get; set; }
        /// <summary>
        /// Страна пользователя.
        /// </summary>
        [JsonProperty("country")]
        public VKCountry Country { get; set; }
        /// <summary>
        /// Город пользователя.
        /// </summary>
        [JsonProperty("city")]
        public VKCity City { get; set; }
        /// <summary>
        /// Счетчики пользователя.
        /// </summary>
        [JsonProperty("counters")]
        public VKProfileCounters Counters { get; set; }
        /// <summary>
        /// Никнейм (отчество) пользователя.
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname {get; set; }
        /// <summary>
        /// Сайт пользователя.
        /// </summary>
        [JsonProperty("site")]
        public string Site { get; set; }
        /// <summary>
        /// Любимая музыка пользователя.
        /// </summary>
        [JsonProperty("music")]
        public string Music { get; set; }
        /// <summary>
        /// Любимые фильмы пользователя.
        /// </summary>
        [JsonProperty("movies")]
        public string Movies { get; set; }
        /// <summary>
        /// Любимые телепередачи пользователя.
        /// </summary>
        [JsonProperty("tv")]
        public string Television { get; set; }
        /// <summary>
        /// Любимые книги пользователя.
        /// </summary>
        [JsonProperty("books")]
        public string Books { get; set; }
        /// <summary>
        /// Любимые игры пользователя.
        /// </summary>
        [JsonProperty("games")]
        public string Games { get; set; }
        /// <summary>
        /// Раздел "о себе".
        /// </summary>
        [JsonProperty("about")]
        public string About { get; set; }
        /// <summary>
        /// Любимые цитаты пользователя.
        /// </summary>
        [JsonProperty("quotes")]
        public string Quotes { get; set; }
        /// <summary>
        /// Деятельность пользователя.
        /// </summary>
        [JsonProperty("activities")]
        public string Activities { get; set; }
        /// <summary>
        /// Интересы пользователя.
        /// </summary>
        [JsonProperty("interests")]
        public string Interests { get; set; }
        /// <summary>
        /// Может ли текущий пользователь оставлять записи
        /// на стене этого пользователя.
        /// </summary>
        [JsonProperty("can_post")]
        public VKBoolean CanPost { get; set; }
        /// <summary>
        /// Разрешено ли видеть чужие записи на 
        /// стене пользователя.
        /// </summary>
        [JsonProperty("can_see_all_posts")]
        public VKBoolean CanSeeAllPosts { get; set; }
        /// <summary>
        /// Разрешено ли писать личные сообщения этому пользователю.
        /// </summary>
        [JsonProperty("can_write_private_message")]
        public VKBoolean CanWritePrivateMessage { get; set; }
        /// <summary>
        /// Разрешено ли видеть чужие аудиозаписи на стене 
        /// пользователя.
        /// </summary>
        [JsonProperty("can_see_audio")]
        public VKBoolean CanSeeAudio { get; set; }
        /// <summary>
        /// Доступно ли комментирование стены.
        /// </summary>
        [JsonProperty("wall_comments")]
        public VKBoolean CanWallComment { get; set; }
        /// <summary>
        /// Информация из раздела "Жизненная позиция".
        /// </summary>
        [JsonProperty("personal")]
        public VKPersonalInfo Personal { get; set; }
        /// <summary>
        /// Род занятия пользователя.
        /// </summary>
        [JsonProperty("occupation")]
        public VKOccupation Occupation { get; set; }
        /// <summary>
        /// Семейное положение пользователя.
        /// </summary>
        [JsonProperty("relation")]
        public VKRelation Relation { get; set; }
        /// <summary>
        /// Телефонные номера пользователя.
        /// </summary>
        [JsonProperty("contacts")]
        public VKUserContacts Contacts { get; set; }
        /// <summary>
        /// Информация о высшем образовании пользователя.
        /// </summary>
        [JsonProperty("education")]
        public VKUserEducation Education { get; set; }
        /// <summary>
        /// Список вузов, в которых учился пользователь.
        /// </summary>
        [JsonProperty("universities")]
        public List<VKUniversity> Universities { get; set; }
        /// <summary>
        /// Список школ, в которых учился пользователь.
        /// </summary>
        [JsonProperty("schools")]
        public List<VKSchool> Schools { get; set; }
        /// <summary>
        /// Время последнего посещения ВКонтакте.
        /// </summary>
        [JsonProperty("last_seen")]
        public VKLastSeen LastSeen { get; set; }

        /// <summary>
        /// Возвращает город и страну пользователя.
        /// </summary>
        public string FullPlace
        {
            get
            {
                if (City != null && Country != null)
                    return String.Format("{0}, {1}", City.Title, Country.Title);
                if (City != null)
                    return City.Title;
                if (Country != null)
                    return Country.Title;
                return String.Empty;
            }
        }
    }
}
