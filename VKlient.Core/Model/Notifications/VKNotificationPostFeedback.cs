using Newtonsoft.Json;
using OneVK.Model.Common;
using System.Collections.Generic;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет информацию об оповещении-посте.
    /// </summary>
    public class VKNotificationPostFeedback : VKNotificationFeedback
    {
        /// <summary>
        /// Коллекция вложения в посте.
        /// </summary>
        [JsonProperty("attachments")]
        public List<VKAttachment> Attachments { get; set; }

        /// <summary>
        /// Информация о местоположении.
        /// </summary>
        [JsonProperty("geo")]
        public VKGeo Geo { get; set; }
    }
}
