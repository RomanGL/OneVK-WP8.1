using Newtonsoft.Json;
using System;

namespace OneVK.Model.Gifts
{
    /// <summary>
    /// Объект подарка ВКонтакте.
    /// </summary>
    public sealed class VKGift : IVKObject
    {
        /// <summary>
        /// Номер подарка.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// URL изображения подарка размером 256x256px.
        /// </summary>
        [JsonProperty("thumb_256")]
        public long Thumb256 { get; set; }

        /// <summary>
        /// URL изображения подарка размером 96x96px.
        /// </summary>
        [JsonProperty("thumb_96")]
        public long Thumb96 { get; set; }

        /// <summary>
        /// URL изображения подарка размером 48x48px.
        /// </summary>
        [JsonProperty("thumb_48")]
        public long Thumb48 { get; set; }
    }
}
