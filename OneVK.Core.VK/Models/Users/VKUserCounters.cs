using Newtonsoft.Json;
using OneVK.Core.VK.Models.Common;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Users
{
    /// <summary>
    /// Счетчики в профиле пользователя ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public class VKProfileCounters : VKCountersBase
    {
        /// <summary>
        /// Количество заметок.
        /// </summary>
        [JsonProperty("notes")]
        public uint NotesCount { get; set; }
        /// <summary>
        /// Количество друзей.
        /// </summary>
        [JsonProperty("friends")]
        public uint FriendsCount { get; set; }
        /// <summary>
        /// Количество друзей в сети.
        /// </summary>
        [JsonProperty("online_friends")]
        public uint OnlineFriendsCount { get; set; }
        /// <summary>
        /// Количество общих друзей.
        /// </summary>
        [JsonProperty("mutual_friends")]
        public uint MutualFriendsCount { get; set; }
        /// <summary>
        /// Количество подписчиков.
        /// </summary>
        [JsonProperty("followers")]
        public uint FollowersCount { get; set; }
        /// <summary>
        /// Количество подписок.
        /// </summary>
        [JsonProperty("subscriptions")]
        public uint SubscriptionsCount { get; set; }
        /// <summary>
        /// Количество страниц.
        /// </summary>
        [JsonProperty("pages")]
        public uint PagesCount { get; set; }
    }
}
