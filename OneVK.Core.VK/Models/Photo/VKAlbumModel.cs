using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using Newtonsoft.Json;
using OneVK.Core.Models.Json;

namespace OneVK.Core.VK.Models.Photo
{
    /// <summary>
    /// Represenets album photo
    /// </summary>
    public class VKAlbumModel:BindableBase
    {
        /// <summary>
        /// Id of album
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Thumb id of album
        /// </summary>
        [JsonProperty("thumb_id")]
        public string ThumbId { get; set; }

        /// <summary>
        /// Owner id of album
        /// </summary>
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }

        /// <summary>
        /// Album title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Album description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Created time
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
        public DateTime Created { get; set; }

        /// <summary>
        /// Updated time
        /// </summary>
        [JsonProperty("updated")]
        [JsonConverter(typeof (UnixtimeToDateTimeConverter))]
        public DateTime Updated { get; set; }

        /// <summary>
        /// Size of album (count)
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// I don't know what does this shit mean
        /// </summary>
        [JsonProperty("thumb_is_last")]
        public int ThumbIsLast { get; set; }

    }
}
