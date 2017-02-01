using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneVK.Enums.Common;
using OneVK.Model.Audio;
using OneVK.Model.Common;
using OneVK.Model.Doc;
using OneVK.Model.Photo;
using OneVK.Model.Video;

namespace OneVK.Model.Message
{
    /// <summary>
    /// Представляет объект вложения в личном сообщении.
    /// </summary>
    public class VKMessageAttachment
    {
        /// <summary>
        /// Объект фотографии, если тип фотография.
        /// </summary>
        [JsonProperty("photo")]
        private VKPhoto Photo { get; set; }

        /// <summary>
        /// Аудиозапись, если тип аудио.
        /// </summary>
        [JsonProperty("audio")]
        private VKAudio Audio { get; set; }

        /// <summary>
        /// Видеозапись, если тип видео.
        /// </summary>
        [JsonProperty("video")]
        private VKVideoBase Video { get; set; }

        /// <summary>
        /// Документ, если тип документ.
        /// </summary>
        [JsonProperty("doc")]
        private VKDocument Document { get; set; }

        /// <summary>
        /// Стикер, если тип стикер.
        /// </summary>
        [JsonProperty("sticker")]
        private VKSticker Sticker { get; set; }

        /// <summary>
        /// Тип вложения.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public VKMessageAttachmentType Type { get; set; }

        /// <summary>
        /// Возвращает объект вложения.
        /// </summary>
        public object Attachment
        {
            get
            {
                switch (Type)
                {
                    case VKMessageAttachmentType.Photo:
                        return Photo;
                    case VKMessageAttachmentType.Video:
                        return Video;
                    case VKMessageAttachmentType.Audio:
                        return Audio;
                    case VKMessageAttachmentType.Doc:
                        return Document;
                    case VKMessageAttachmentType.Wall:
                        break;
                    case VKMessageAttachmentType.Wall_reply:
                        break;
                    case VKMessageAttachmentType.Sticker:
                        return Sticker;
                    case VKMessageAttachmentType.Gift:
                        break;
                }
                return null;
            }
            set
            {
                switch (Type)
                {
                    case VKMessageAttachmentType.Photo:
                        Photo = (VKPhoto)value;
                        break;
                    case VKMessageAttachmentType.Video:
                        Video = (VKVideoBase)value;
                        break;
                    case VKMessageAttachmentType.Audio:
                        Audio = (VKAudio)value;
                        break;
                    case VKMessageAttachmentType.Doc:
                        Document = (VKDocument)value;
                        break;
                    case VKMessageAttachmentType.Wall:
                        break;
                    case VKMessageAttachmentType.Wall_reply:
                        break;
                    case VKMessageAttachmentType.Sticker:
                        Sticker = (VKSticker)value;
                        break;
                    case VKMessageAttachmentType.Gift:
                        break;
                }
            }
        }
    }
}
