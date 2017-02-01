using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Enums.Group;

namespace OneVK.Model.Group
{
    /// <summary>
    /// Представляет краткую информацию о группе.
    /// </summary>
    public class VKGroupShort
    {
        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        [JsonProperty("id")]
        public ulong ID { get; set; }
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
        /// Является ли пользователь администратором сообщества.
        /// </summary>
        [JsonProperty("id_admin")]
        public VKUserIsAdmin IsAdmin { get; set; }
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
    }
}
