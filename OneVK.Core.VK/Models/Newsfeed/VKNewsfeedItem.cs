using Newtonsoft.Json;
using OneVK.Core.Models.Json;
using OneVK.Core.VK.Json;
using OneVK.Core.VK.Models.Common;
using Microsoft.Practices.Prism.StoreApps;
using System;
using PropertyChanged;

namespace OneVK.Core.VK.Models.Newsfeed
{
    /// <summary>
    /// Представляет элемент ленты новостей ВКонтакте.
    /// </summary>
    [ImplementPropertyChanged]
    public class VKNewsfeedItem : BindableBase
    {
        /// <summary>
        /// Тип элемента новостной ленты.
        /// </summary>
        [JsonProperty("type")]
        public VKNewsfeedItemType Type { get; set; }
        
        /// <summary>
        /// Идентификатор источника новости.
        /// </summary>
        [JsonProperty("source_id")]
        public long SourceID { get; set; }
        
        /// <summary>
        /// Время публикации новости.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Идентификатор записи на стене.
        /// </summary>
        [JsonProperty("post_id")]
        public long PostID { get; set; }
        
        /// <summary>
        /// Идентификатор владельца, у которого скопирована запись.
        /// </summary>
        [JsonProperty("copy_owner_id")]
        public long CopyOwnerID { get; set; }
        
        /// <summary>
        /// Идентификатор оригинальной записи на стене владельца.
        /// </summary>
        [JsonProperty("copy_post_id")]
        public long CopyPostID { get; set; }
        
        /// <summary>
        /// Дата публикации оригинальной записи.
        /// </summary>
        [JsonProperty("copy_post_date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime CopyPostDate { get; set; }
        
        /// <summary>
        /// Текст записи.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        
        /// <summary>
        /// Может ли пользователь редактировать запись.
        /// </summary>
        [JsonProperty("can_edit")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanEdit { get; set; }
        
        /// <summary>
        /// Может ли пользователь удалить запись.
        /// </summary>
        [JsonProperty("can_delete")]
        [JsonConverter(typeof(VKBooleanConverter))]
        public bool CanDelete { get; set; }
        
        /// <summary>
        /// Информация о комментариях к записи.
        /// </summary>
        public VKComments Comments { get; set; }
        
        /// <summary>
        /// Информация об отметках Мне нравится.
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes { get; set; }
        
        /// <summary>
        /// Информация о репостах.
        /// </summary> 
        [JsonProperty("reposts")]
        public VKReposts Reposts { get; set; }

        /// <summary>
        /// Владелец объекта.
        /// </summary>
        public IVKItemOwner Owner { get; set; }
    }
}
