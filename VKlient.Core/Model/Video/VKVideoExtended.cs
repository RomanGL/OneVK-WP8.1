using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Enums.Video;
using OneVK.Model.Common;

namespace OneVK.Model.Video
{
    /// <summary>
    /// Видеозапись ВКонтакте с расширенными данными.
    /// </summary>
    public class VKVideoExtended : VKVideoBase
    {
        /// <summary>
        /// Настройки приватности в формате настроек приватности (возвращаются только для текущего пользователя).
        /// </summary>
        [JsonProperty("privacy_view")]
        public VKAlbumPrivacy PrivacyView { get; set; }
        /// <summary>
        /// Настройки приватности в формате настроек приватности (возвращаются только для текущего пользователя).
        /// </summary>
        [JsonProperty("privacy_comment")]
        public VKAlbumPrivacy PrivacyComment { get; set; }
        /// <summary>
        /// Может ли пользовать оставить комментарий к видеозаписи.
        /// </summary>
        [JsonProperty("can_comment")]
        public VKBoolean CanComment { get; set; }
        /// <summary>
        /// Может ли пользователь сделать репост видеозаписи.
        /// </summary>
        [JsonProperty("can_repost")]
        public VKBoolean CanRepost { get; set; }
        /// <summary>
        /// Информация об отметках «Мне нравится».
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes { get; set; }
        /// <summary>
        /// Зациклена ли видеозапись.
        /// </summary>
        [JsonProperty("repeat")]
        public VKVideoRepeat Repeat { get; set; }
    }
}
