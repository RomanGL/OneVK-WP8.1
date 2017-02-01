using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneVK.Enums.Common;
using OneVK.Enums.Profile;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Базовый профиль пользователя.
    /// Требует доработки.
    /// </summary>
    public class VKProfileBase : IVKItemOwner
    {
        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public string FullName { get { return FirstName + " " + LastName; } }        
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
        /// Идентификатор пользователя.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }
        /// <summary>
        /// Статус пользователя.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// Квадратная фотография пользователя размером 50px.
        /// </summary>
        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
        /// <summary>
        /// Квадратная фотография пользователя размером 100px.
        /// </summary>
        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }
        /// <summary>
        /// Квадратная фотография пользователя размером 200px.
        /// </summary>
        [JsonProperty("photo_200")]
        public string Photo200 { get; set; }
        /// <summary>
        /// Находится ли пользователь на сайте.
        /// </summary>
        [JsonProperty("online")]
        public VKBoolean Online { get; set; }
        /// <summary>
        /// Зашел ли пользователь с мобильного устройства.
        /// </summary>
        [JsonProperty("online_mobile")]
        public VKBoolean OnlineMobile { get; set; }
        /// <summary>
        /// Деактивирована ли страница пользователя.
        /// </summary>
        [JsonProperty("deactivated")]
        [JsonConverter(typeof(StringEnumConverter))]
        public VKIsDeactivated Deactivated { get; set; }
        /// <summary>
        /// Пол пользователя.
        /// </summary>
        [JsonProperty("sex")]
        public VKUserSex Sex { get; set; }

        #region IVKItemOwner
        /// <summary>
        /// Возвращает полное имя пользователя.
        /// </summary>
        public string Title { get { return FullName; } }

        /// <summary>
        /// Возвращает ссылку на аватар пользователя.
        /// </summary>
        public string PhotoURL { get { return Photo50; } }
        /// <summary>
        /// Идентификатор элемента.
        /// </summary>
        long IVKItemOwner.ID
        {
            get { return (long)ID; }
            set { ID = (ulong)value; }
        }
        #endregion        
    }
}
