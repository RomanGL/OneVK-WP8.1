using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Enums.Group;

namespace OneVK.Model.Group
{
    /// <summary>
    /// Базовое сообщество ВКонтакте.
    /// </summary>
#if ONEVK_CORE
    public class VKGroupBase : IVKItemOwner
#else
    public class VKGroupBase
#endif
    {
        /// <summary>
        /// Название сообщества.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Короткий адрес сообщества.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
        /// <summary>
        /// Является ли сообщество закрытым.
        /// </summary>
        [JsonProperty("is_closed")]
        public VKGroupIsClosed IsClosed { get; set; }
        /// <summary>
        /// Является ли сообщество деактивированным.
        /// </summary>
        [JsonProperty("deactivated")]
        public VKIsDeactivated IsDeactivated { get; set; }
        /// <summary>
        /// Является ли пользователь администратором сообщества.
        /// </summary>
        [JsonProperty("id_admin")]
        public VKBoolean IsAdmin { get; set; }
        /// <summary>
        /// Полномочия пользователя в сообществе.
        /// </summary>
        [JsonProperty("admin_level")]
        public VKUserIsAdmin AdminLevel { get; set; }
        /// <summary>
        /// Является ли пользователь участником сообщества.
        /// </summary>
        [JsonProperty("is_member")]
        public VKBoolean IsMember { get; set; }
        /// <summary>
        /// Квадратная фотография сообщества размером 50 пикс.
        /// </summary>
        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
        /// <summary>
        /// Квадратная фотография сообщества размером 100 пикс.
        /// </summary>
        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }
        /// <summary>
        /// Квадратная фотография сообщества размером 200 пикс.
        /// </summary>
        [JsonProperty("photo_200")]
        public string Photo200 { get; set; }
        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }

#if ONEVK_CORE
        #region IVKItemOwner
        /// <summary>
        /// Возвращает название сообщества.
        /// </summary>
        public string Title { get { return Name; } }

        /// <summary>
        /// Возвращает ссылку на аватарку сообщества.
        /// </summary>
        public string PhotoURL { get { return Photo100; } }        

        /// <summary>
        /// Идентификатор элемента.
        /// </summary>
        long IVKItemOwner.ID
        {
            get { return (long)ID; }
            set { ID = (ulong)value; }
        }
        #endregion
#endif
    }
}
