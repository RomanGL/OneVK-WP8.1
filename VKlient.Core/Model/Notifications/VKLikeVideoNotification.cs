﻿using Newtonsoft.Json;
using OneVK.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение об одном или нескольких новых отметках Мне нравится к видеозаписи пользователя.
    /// </summary>
    public class VKLikeVideoNotification : VKNotificationBase,
        INotificationParent<VKNotificationVideo>,
        INotificationFeedback<VKCountedItemsObject<VKNotificationActionObjectsContainer>>
    {
        /// <summary>
        /// Пост, у которого появились новые отметки Мне нравится.
        /// </summary>
        [JsonProperty("parent")]
        public VKNotificationVideo Parent { get; set; }

        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        [JsonProperty("feedback")]
        public VKCountedItemsObject<VKNotificationActionObjectsContainer> Feedback { get; set; }
    }
}
