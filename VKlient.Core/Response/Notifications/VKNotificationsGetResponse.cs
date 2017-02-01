using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Model.Notifications;
using System;
using System.Collections.Generic;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос получения списка оповещений для пользователя.
    /// </summary>
    [JsonConverter(typeof(VKNotificationsGetResponseConverter))]
    public class VKNotificationsGetResponse : VKCountedItemsObject<IVKNotification>
    {
        /// <summary>
        /// Профили пользователей, используемые в ответе.
        /// </summary>
        [JsonProperty("profiles")]
        public List<VKNotificationProfile> Profiles { get; set; }

        /// <summary>
        /// Сообщества, используемые в ответе.
        /// </summary>
        [JsonProperty("groups")]
        public List<VKNotificationGroup> Groups { get; set; }

        /// <summary>
        /// Дата последнего просмотра.
        /// </summary>
        [JsonProperty("last_viewed")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime LastViewed { get; set; }

        /// <summary>
        /// Строка для получения следующей группы оповещений.
        /// </summary>
        [JsonProperty("next_from")]
        public string NextFrom { get; set; }
    }
}
