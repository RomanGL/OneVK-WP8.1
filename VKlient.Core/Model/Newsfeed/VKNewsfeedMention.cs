using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Core.Json;
using OneVK.Model.Common;

namespace OneVK.Model.Newsfeed
{
    /// <summary>
    /// Объект в списке записей пользователей на своих стенах, 
    /// в которых упоминается указанный пользователь.
    /// </summary>
    public class VKNewsfeedMention : IVKObject
    {
        /// <summary>
        /// Идентификатор записи на стене пользователя.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Идентификатор пользователя, написавшего запись.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; }

        /// <summary>
        /// Время публикации записи.
        /// </summary>
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        [JsonProperty("date")]
        public DateTime Dat { get; set; }

        /// <summary>
        /// ИНформация об отметках "Мне нравится".
        /// </summary>
        [JsonProperty("likes")]
        public VKLikes Likes { get; set; }

        /// <summary>
        /// Объект, прикреплен к текущей новости.
        /// </summary>
        [JsonProperty("attachment")]
        public VKAttachment Attachment { get; set; }

        /// <summary>
        /// Информация о комментариях к записи.
        /// </summary>
        [JsonProperty("comments")]
        public VKComments Comments { get; set; }

        /// <summary>
        /// Информация о местоположении, прикрепленная к записи.
        /// </summary>
        [JsonProperty("geo")]
        public VKGeo Geo { get; set; }

        /// <summary>
        /// Идентификатор владельца стены, с которой была скопирована запись.
        /// </summary>
        [JsonProperty("copy_owner_id")]
        public long CopyOwnerID { get; set; }

        /// <summary>
        /// Идентификатор скопированной записи на стене его владельца.
        /// </summary>
        [JsonProperty("copy_post_id")]
        public long CopyPostID { get; set; }
    }
}
