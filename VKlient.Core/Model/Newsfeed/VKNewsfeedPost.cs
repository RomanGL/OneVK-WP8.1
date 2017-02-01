using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using OneVK.Core.Json;
using OneVK.Enums.Newsfeed;
using OneVK.Model.Photo;
using OneVK.Model.Wall;
using OneVK.Response;

namespace OneVK.Model.Newsfeed
{
    /// <summary>
    /// Базовый объект записей в новостях.
    /// </summary>
    public class VKNewsfeedPost : BaseVKPost
    {      
        /// <summary>
        /// Список, к которому относится запись.
        /// </summary>        
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public VKNewsfeedFilters Type { get; set; }

        /// <summary>
        /// Идентификатор записи на стене владельца.
        /// </summary>
        [JsonProperty("post_id")]
        public override ulong ID { get; set; }

        /// <summary>
        /// Идентификатор владельца стены, на котором размещена запись.
        /// </summary>
        [JsonProperty("source_id")]
        public override long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор владельца поста, в случае, если он находится в copy_history текущего поста.
        /// </summary>
        [JsonProperty("owner_id")]
        public long CopyHistoryOwnerID { get; set; }

        /// <summary>
        /// Идентификатор владельца стены, у которого была скопирована запись.
        /// </summary>
        [JsonProperty("copy_owner_id")]
        public long CopyOwnerID { get; set; }

        /// <summary>
        /// Идентификатор записи на стене пользователя,
        /// у которго была скопирована запись.
        /// </summary>
        [JsonProperty("copy_post_id")]
        public long CopyPostID { get; set; }

        /// <summary>
        /// Дата скопированной записи.
        /// </summary>
        [JsonProperty("copy_post_date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime CopyPostDate { get; set; }

        /// <summary>
        /// Коллекция фотографий, если тип поста wall_photo.
        /// </summary>
        [JsonProperty("photos")]
        public VKCountedItemsObject<VKPhoto> Photos { get; set; }
    }
}
