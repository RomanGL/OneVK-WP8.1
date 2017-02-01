using Microsoft.Practices.Prism.StoreApps;
using Newtonsoft.Json;
using OneVK.Core.VK.Models.Common;
using PropertyChanged;
using OneVK.Core.VK.Json;

namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Представляет сообщество ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public class VKGroup : BindableBase, IVKItemOwner
    {
        /// <summary>
        /// Идентификатор сообщества.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }
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
        /// Заблокировано ли сообщество.
        /// </summary>
        [JsonProperty("deactivated")]
        public VKDeactivated Deactivated { get; set; }
        /// <summary>
        /// Является ли текущий пользователь руководителем.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Полномочия текущего пользователя.
        /// </summary>
        [JsonProperty("admin_level")]
        public VKUserIsAdmin AdminLevel { get; set; }
        /// <summary>
        /// Является ли текущий пользователь участником сообщества.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("is_member")]
        public bool IsMember { get; set; }
        /// <summary>
        /// Статус участника текущего пользователя.
        /// </summary>
        [JsonProperty("member_status")]
        public VKUserMemberStatus MemberStatus { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который отправил приглашение.
        /// </summary>
        [JsonProperty("invited_by")]
        public long InvitedBy { get; set; }
        /// <summary>
        /// Тип сообщества.
        /// </summary>
        [JsonProperty("type")]
        public VKGroupType Type { get; set; }
        /// <summary>
        /// Установлена ли у сообщества фотография.
        /// </summary>
        [JsonConverter(typeof(VKBooleanConverter))]
        [JsonProperty("has_photo")]
        public bool HasPhoto { get; set; }
        /// <summary>
        /// Фотография размером 50.
        /// </summary>
        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
        /// <summary>
        /// Фотография размером 100.
        /// </summary>
        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }
        /// <summary>
        /// Фотография в максимальном размере.
        /// </summary>
        [JsonProperty("photo_200")]
        public string Photo200 { get; set; }
    }
}
