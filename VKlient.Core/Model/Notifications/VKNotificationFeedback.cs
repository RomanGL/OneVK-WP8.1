using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Model.Common;
using System;

namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет базовую информацию об оповещении.
    /// </summary>
    public class VKNotificationFeedback : IActionObjectContainer
    {
        /// <summary>
        /// Идентификатор записи-ответа.
        /// </summary>
        [JsonProperty("id")]
        public uint ID { get; set; }
        
        /// <summary>
        /// Идентификатор владельца стены, на которой размещена запись.
        /// </summary>
        [JsonProperty("to_id")]
        public long ToID { get; set; }

        /// <summary>
        /// Идентификатор автора ответа.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; } 
        
        /// <summary>
        /// Текст ответа.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Находится в записях со стен и содержит информацию о 
        /// числе людей, которым понравилась данная запись.
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes { get; set; }   
        
        /// <summary>
        /// Объект-инициатор оповещения.
        /// </summary>
        [JsonProperty("action_object")]
        [JsonConverter(typeof(NotificationActionObjectConverter))]
        public INotificationActionObject ActionObject { get; set; } 
    }
}
