using System;
using Newtonsoft.Json;
using OneVK.Enums.Common;
using OneVK.Enums.Notifications;
using OneVK.Model.Profile;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет профиль пользователя оповещения.
    /// </summary>
    public class VKNotificationProfile : VKProfileShort, INotificationActionObject
    {
        /// <summary>
        /// Короткий адрес страницы.
        /// </summary>
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// Ссылка на фотографию размером 200 px.
        /// </summary>
        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }

        /// <summary>
        /// Идентификатор приложения, с которого пользователь вошел на сайт.
        /// </summary>
        [JsonProperty("online_app")]
        public ulong OnlineAppID { get; set; }

        /// <summary>
        /// Пользователь находится на сайте с мобильного устройства.
        /// </summary>
        [JsonProperty("online_mobile")]
        public VKBoolean OnlineMobile { get; set; }

        /// <summary>
        /// Тип объекта действия.
        /// </summary>
        [JsonProperty("type")]
        public ActionObjectType Type { get { return ActionObjectType.User; } }

        /// <summary>
        /// Заголовок объекта действия.
        /// </summary>
        [JsonIgnore]
        public string Title { get { return FullName; } }
    }
}
