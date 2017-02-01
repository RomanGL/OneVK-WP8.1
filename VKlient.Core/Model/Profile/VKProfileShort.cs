using Newtonsoft.Json;
using OneVK.Enums.Common;
using System;
using OneVK.Enums.Profile;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Представляет краткий профиль пользователя.
    /// </summary>
    public class VKProfileShort : IEquatable<VKProfileShort>, IVKProfileShort
    {
        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        [JsonIgnore]
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
        /// Квадратная фотография пользователя размером 50px.
        /// </summary>
        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
        /// <summary>
        /// Находится ли пользователь на сайте.
        /// </summary>
        [JsonProperty("online")]
        public VKBoolean Online { get; set; }
        /// <summary>
        /// Пол пользователя.
        /// </summary>
        [JsonProperty("sex")]
        public VKUserSex Sex { get; set; }

        /// <summary>
        /// Сравнивает экземпляр с текущим.
        /// </summary>
        /// <param name="other">Экземпляр для сравнения.</param>
        public bool Equals(VKProfileShort other)
        {
            return other.ID == this.ID;
        }

        /// <summary>
        /// Возвращает хэш-код экземпляра.
        /// </summary>
        public override int GetHashCode()
        {
            return FullName.GetHashCode() + ID.GetHashCode();
        }
    }
}
