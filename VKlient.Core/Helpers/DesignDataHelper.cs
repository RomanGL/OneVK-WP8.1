using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Model.Newsfeed;
using OneVK.Model.Profile;
using OneVK.Model.Photo;
using OneVK.Model.Audio;
using OneVK.Model.Video;
using OneVK.Model.Common;
using OneVK.Enums.Common;
using OneVK.Model.Message;
using OneVK.Enums.Message;

namespace OneVK.Helpers
{
#if DEBUG
    /// <summary>
    /// Представляет помощник данных времени разработки.
    /// </summary>
    public static class DesignDataHelper
    {
        public static VKDialog GetReadedSentDialog()
        {
            return new VKDialog
            {
                Message = new VKDialogMessage
                {
                    Title = "Роман Гладких",
                    Body = "Это отправленное сообщение.",
                    ReadState = VKBoolean.True,
                    Type = VKMessageType.Sent
                }
            };
        }

        public static VKDialog GetUnreadSentDialog()
        {
            return new VKDialog
            {
                Message = new VKDialogMessage
                {
                    Title = "Роман Гладких",
                    Body = "Это отправленное сообщение.",
                    ReadState = VKBoolean.False,
                    Type = VKMessageType.Sent
                }
            };
        }

        public static VKDialog GetUnreadDialog()
        {
            return new VKDialog
            {
                Unread = 5,
                Message = new VKDialogMessage
                {
                    Title = "Роман Гладких",
                    Body = "Это полученное сообщение.",
                    ReadState = VKBoolean.False,
                    Type = VKMessageType.Received
                }
            };
        }

        public static VKDialog GetReadedDialog()
        {
            return new VKDialog
            {
                Message = new VKDialogMessage
                {
                    Title = "Роман Гладких",
                    Body = "Это полученное сообщение.",
                    ReadState = VKBoolean.True,
                    Type = VKMessageType.Received
                }
            };
        }

        public static VKDialog GetReadedSentChatDialog()
        {
            return new VKDialog
            {
                Message = new VKDialogMessage
                {
                    Title = "OneVK Chat",
                    Body = "Это отправленное сообщение.",
                    ReadState = VKBoolean.True,
                    Type = VKMessageType.Sent
                }
            };
        }

        public static VKDialog GetUnreadSentChatDialog()
        {
            return new VKDialog
            {
                Unread = 5,
                Message = new VKDialogMessage
                {
                    Title = "OneVK Chat",
                    Body = "Это отправленное сообщение.",
                    ReadState = VKBoolean.False,
                    Type = VKMessageType.Sent
                }
            };
        }

        public static VKDialog GetReadedChatDialog()
        {
            return new VKDialog
            {
                Message = new VKDialogMessage
                {
                    Title = "OneVK Chat",
                    Body = "Это полученное сообщение.",
                    ReadState = VKBoolean.True,
                    Type = VKMessageType.Received
                }
            };
        }

        public static VKDialog GetUnreadChatDialog()
        {
            return new VKDialog
            {
                Unread = 5,
                Message = new VKDialogMessage
                {
                    Title = "OneVK Chat",
                    Body = "Это полученное сообщение.",
                    ReadState = VKBoolean.False,
                    Type = VKMessageType.Received
                }
            };
        }

        /// <summary>
        /// Возвращает демонстрационный экземпляр фотографии.
        /// </summary>
        public static VKPhotoExtended GetPhoto()
        {
            return new VKPhotoExtended
            {
                Photo130 = "https://prcdn.500px.org/photos/19114143/w%3D600_h%3D600_t%3Dfalse/ee4a751b1aaef8f522c71b35c2ab2fa0efb135a6",
                Photo604 = "https://prcdn.500px.org/photos/19114143/w%3D600_h%3D600_t%3Dfalse/ee4a751b1aaef8f522c71b35c2ab2fa0efb135a6",
                Width = 400,
                Height = 600
            };
        }

        /// <summary>
        /// Возвращает демонстрационный экземпляр фотоальбома.
        /// </summary>
        public static VKPhotoAlbum GetPhotoAlbum()
        {
            return new VKPhotoAlbum
            {
                Title = "Лето",
                Thumbnail = "https://prcdn.500px.org/photos/19114143/w%3D600_h%3D600_t%3Dfalse/ee4a751b1aaef8f522c71b35c2ab2fa0efb135a6"
            };
        }

        /// <summary>
        /// Возвращает демонстрационный экземпляр фотографии.
        /// </summary>
        public static VKAudio GetAudio()
        {
            return new VKAudio
            {
                Title = "Toxicity",
                Artist = "System Of A Down"
            };
        }

        /// <summary>
        /// Возвращает демонстрационный экземпляр поста новостной ленты.
        /// </summary>
        public static VKNewsfeedPost GetNewsfeedPost()
        {
            return new VKNewsfeedPost
                {
                    FullText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    Owner = new VKProfileBase { FirstName = "Pavel", LastName = "Durov", Photo50 = "https://vk.com/images/stickers/61/64.png" },
                    Likes = new VKLikes { Count = 356, UserLikes = VKBoolean.True },
                    Reposts = new VKReposts { Count = 12, UserReposted = VKBoolean.False },
                    Attachments = new List<VKAttachment>
                    {
                        new VKAttachment { Type = VKAttachmentType.Photo, Attachment = GetPhoto() },
                        new VKAttachment { Type = VKAttachmentType.Photo, Attachment = GetPhoto() },
                        new VKAttachment { Type = VKAttachmentType.Photo, Attachment = GetPhoto() },
                        new VKAttachment { Type = VKAttachmentType.Photo, Attachment = GetPhoto() }
                    }
                };
        }
    }
#endif
}
