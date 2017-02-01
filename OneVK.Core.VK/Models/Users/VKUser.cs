using Newtonsoft.Json;
using OneVK.Core.VK.Json;
using OneVK.Core.VK.Models.Common;
using Microsoft.Practices.Prism.StoreApps;
using PropertyChanged;
using System;
using System.Collections.Generic;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Представляет профиль пользователя ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public class VKUser : BindableBase, IVKItemOwner
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
        
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public string Name { get { return $"{FirstName} {LastName}"; } }
        
        /// <summary>
        /// Информация о деактивации страницы пользователя.
        /// </summary>
        [JsonProperty("deactivated")]
        public VKDeactivated Deactivated { get; set; }
        
        /// <summary>
        /// Верифицирована ли страница пользователя.
        /// </summary>
        [JsonProperty("verified")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool Verified { get; set; }
        
        /// <summary>
        /// Пол пользователя.
        /// </summary>
        [JsonProperty("sex")]
        public VKUserSex Sex { get; set; }
        
        /// <summary>
        /// Дата дня рождения.
        /// </summary>
        [JsonProperty("bdate")]
        [JsonConverter(typeof(VKBirthdayDateToDateTimeConverter))]
        public DateTime? BirthdayDate { get; set; }
        
        /// <summary>
        /// Город пользователя.
        /// </summary>
        [JsonProperty("city")]
        public VKCity City { get; set; }
        
        /// <summary>
        /// Страна пользователя.
        /// </summary>
        [JsonProperty("country")]
        public VKCountry Country { get; set; }
        
        /// <summary>
        /// Название родного города пользователя.
        /// </summary>
        [JsonProperty("home_town")]
        public string HomeTown { get; set; }
        
        /// <summary>
        /// Квардратная фотография размером 50.
        /// </summary>
        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
        
        /// <summary>
        /// Квардратная фотография размером 100.
        /// </summary>
        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }
        
        /// <summary>
        /// Фотография шириной 200.
        /// </summary>
        [JsonProperty("photo_200_orig")]
        public string Photo200Orig { get; set; }
        
        /// <summary>
        /// Квадратная фотография размером 200.
        /// </summary>
        [JsonProperty("photo_200")]
        public string Photo200 { get; set; }
        
        /// <summary>
        /// Фотография шириной 400.
        /// </summary>
        [JsonProperty("photo_400_orig")]
        public string Photo400Orig { get; set; }
        
        /// <summary>
        /// Квадратная фотография с максимальной шириной.
        /// </summary>
        [JsonProperty("photo_max")]
        public string PhotoMax { get; set; }
        
        /// <summary>
        /// Фотография с максимальной шириной.
        /// </summary>
        [JsonProperty("photo_max_orig")]
        public string PhotoMaxOrig { get; set; }
        
        /// <summary>
        /// Находится ли пользователь в сети.
        /// </summary>
        [JsonProperty("online")]
        public bool Online { get; set; }
        
        /// <summary>   
        /// Находится ли пользователь в сети с мобильного устройства.
        /// </summary>
        [JsonProperty("online_mobile")]
        public bool OnlineMobile { get; set; }
        
        /// <summary>
        /// Идентификатор приложения, с которого зашел пользователь.
        /// </summary>
        [JsonProperty("online_app")]
        public uint OnlineApp { get; set; }
        
        /// <summary>
        /// Короткий адрес страницы.
        /// </summary>
        [JsonProperty("domain")]
        public string Domain { get; set; }
        
        /// <summary>
        /// Известен ли номер моильного телефона пользователя.
        /// </summary>
        [JsonProperty("has_mobile")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool HasMobile { get; set; }
        
        /// <summary>
        /// Контакты пользователя.
        /// </summary>
        [JsonProperty("contacts")]
        public VKUserContacts Contacts { get; set; }
        
        /// <summary>
        /// Сайт пользователя.
        /// </summary>
        [JsonProperty("site")]
        public string Site { get; set; }
        
        /// <summary>
        /// Информация о высшем образовании пользователя.
        /// </summary>
        [JsonProperty("education")]
        public VKUserEducation Education { get; set; }
        
        /// <summary>
        /// Список ВУЗов пользователя.
        /// </summary>
        [JsonProperty("universities")]
        public List<VKUniversity> Universities { get; set; }
        
        /// <summary>
        /// Список школ пользователя.
        /// </summary>
        [JsonProperty("schools")]
        public List<VKSchool> Schools { get; set; }

        /// <summary>
        /// Никнейм (отчество) пользователя.
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Счетчики пользователя.
        /// </summary>
        [JsonProperty("counters")]
        public VKProfileCounters Counters { get; set; }

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
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanPost { get; set; }

        /// <summary>
        /// Разрешено ли видеть чужие записи на 
        /// стене пользователя.
        /// </summary>
        [JsonProperty("can_see_all_posts")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanSeeAllPosts { get; set; }

        /// <summary>
        /// Разрешено ли писать личные сообщения этому пользователю.
        /// </summary>
        [JsonProperty("can_write_private_message")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanWritePrivateMessage { get; set; }

        /// <summary>
        /// Разрешено ли видеть чужие аудиозаписи на стене 
        /// пользователя.
        /// </summary>
        [JsonProperty("can_see_audio")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanSeeAudio { get; set; }

        /// <summary>
        /// Доступно ли комментирование стены.
        /// </summary>
        [JsonProperty("wall_comments")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanWallComment { get; set; }

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
        /// Время последнего посещения ВКонтакте.
        /// </summary>
        [JsonProperty("last_seen")]
        public VKLastSeen LastSeen { get; set; }
    }
}
