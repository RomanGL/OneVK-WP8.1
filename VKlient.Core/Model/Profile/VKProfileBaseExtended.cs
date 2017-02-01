using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;
using OneVK.Model.Common;
using Newtonsoft.Json;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Представляет расширенный базовый профиль пользователя.
    /// </summary>
    public class VKProfileBaseExtended : VKProfileBase
    {
        [JsonProperty("bdate")]
        private string _birthdayDate { get; set; }

        /// <summary>
        /// Дата дня рождения пользователя.
        /// </summary>  
        public DateTime BirthdayDate
        {
            get { return DateTime.Parse(_birthdayDate); }
            set { _birthdayDate = value.ToString("dd.MM.yyy"); }
        }
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
        /// Время последнего посещения ВКонтакте.
        /// </summary>
        [JsonProperty("last_seen")]
        public VKLastSeen LastSeen { get; set; }
        /// <summary>
        /// Счетчики пользователя.
        /// </summary>
        [JsonProperty("counters")]
        public VKProfileCounters Counters { get; set; }

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
