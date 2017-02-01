using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneVK.Enums.Common;

namespace OneVK.Model.Comment
{
    /// <summary>
    /// Представляет вложение в комментарий.
    /// </summary>
    public class VKCommentAttachment
    {
        /// <summary>
        /// Тип вложения.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public VKCommentAttachmentType Type { get; set; }
    }
}
