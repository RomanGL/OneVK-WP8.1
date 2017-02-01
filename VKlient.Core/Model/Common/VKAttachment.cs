using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using OneVK.Enums.Common;
using OneVK.Model.Photo;
using OneVK.Model.Audio;
using OneVK.Model.Video;
using OneVK.Model.Doc;
using OneVK.Model.Polls;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет собой вложение в запись.
    /// Объект содержимого зависит от типа вложения.
    /// </summary>
    public sealed class VKAttachment
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
        /// Ссылка, если тип ссылка.
        /// </summary>
        [JsonProperty("link")]
        private VKLink Link { get; set; }

        // <summary>
        /// Опрос, если тип опрос.
        /// </summary>
        [JsonProperty("poll")]
        private VKPoll Poll { get; set; }

        /// <summary>
        /// Документ, если тип документ.
        /// </summary>
        [JsonProperty("doc")]
        private VKDocument Document { get; set; }               

        /// <summary>
        /// Тип вложения.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public VKAttachmentType Type { get; set; }

        /// <summary>
        /// Объект вложения.
        /// </summary>
        public object Attachment
        {
            get
            {
                switch (Type)
                {
                    case VKAttachmentType.Photo:
                        return Photo;
                    case VKAttachmentType.Posted_photo:
                        break;
                    case VKAttachmentType.Video:
                        return Video;
                    case VKAttachmentType.Audio:
                        return Audio;
                    case VKAttachmentType.Doc:
                        return Document;
                    case VKAttachmentType.Graffiti:
                        break;
                    case VKAttachmentType.Link:
                        return Link;
                    case VKAttachmentType.Note:
                        break;
                    case VKAttachmentType.App:
                        break;
                    case VKAttachmentType.Poll:
                        return Poll;
                    case VKAttachmentType.Page:
                        break;
                    case VKAttachmentType.Album:
                        break;
                    case VKAttachmentType.Photos_list:
                        break;
                }
                return null;
            }
            internal set
            {
                switch (Type)
                {
                    case VKAttachmentType.Photo:
                        Photo = (VKPhoto)value;
                        break;
                    case VKAttachmentType.Posted_photo:
                        break;
                    case VKAttachmentType.Video:
                        Video = (VKVideoBase)value;
                        break;
                    case VKAttachmentType.Audio:
                        Audio = (VKAudio)value;
                        break;
                    case VKAttachmentType.Doc:
                        Document = (VKDocument)value;
                        break;
                    case VKAttachmentType.Graffiti:
                        break;
                    case VKAttachmentType.Link:
                        Link = (VKLink)value;
                        break;
                    case VKAttachmentType.Note:
                        break;
                    case VKAttachmentType.App:
                        break;
                    case VKAttachmentType.Poll:
                        Poll = (VKPoll)value;
                        break;
                    case VKAttachmentType.Page:
                        break;
                    case VKAttachmentType.Album:
                        break;
                    case VKAttachmentType.Photos_list:
                        break;
                }
            }
        }
    }
}