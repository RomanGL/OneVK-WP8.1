using OneVK.Enums.Notifications;
using OneVK.Model.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OneVK.Core.Xaml
{
    /// <summary>
    /// Представляет логику выбора шаблона для оповещения ВКонтакте.
    /// </summary>
    public sealed class NotificationTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LikePhotoVideoTemplate { get; set; }
        public DataTemplate LikeCommentTemplate { get; set; }
        public DataTemplate LikeCommentPhotoVideoTemplate { get; set; }
        public DataTemplate LikePostTemplate { get; set; }
        public DataTemplate FollowTemplate { get; set; }
        public DataTemplate FriendAccepted { get; set; }
        public DataTemplate ReplyCommentTemplate { get; set; }
        public DataTemplate ReplyCommentPhotoVideoTemplate { get; set; }
        public DataTemplate MentionTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var n = (IVKNotification)item;
            switch (n.Type)
            {
                case VKNotificationType.follow: return FollowTemplate;
                case VKNotificationType.friend_accepted: return FriendAccepted;
                case VKNotificationType.mention: return MentionTemplate;
                case VKNotificationType.mention_comments:
                    break;
                case VKNotificationType.wall:
                    break;
                case VKNotificationType.wall_publish:
                    break;
                case VKNotificationType.comment_post:
                    break;
                case VKNotificationType.comment_photo:
                    break;
                case VKNotificationType.comment_video:
                    break;
                case VKNotificationType.reply_comment: return ReplyCommentTemplate;
                case VKNotificationType.reply_comment_photo: return ReplyCommentPhotoVideoTemplate;
                case VKNotificationType.reply_comment_video: return ReplyCommentPhotoVideoTemplate;
                case VKNotificationType.reply_topic:
                    break;
                case VKNotificationType.like_post: return LikePostTemplate;
                case VKNotificationType.like_comment: return LikeCommentTemplate;
                case VKNotificationType.like_photo: return LikePhotoVideoTemplate;
                case VKNotificationType.like_video: return LikePhotoVideoTemplate;
                case VKNotificationType.like_comment_photo: return LikeCommentPhotoVideoTemplate;
                case VKNotificationType.like_comment_video: return LikeCommentPhotoVideoTemplate;
                case VKNotificationType.like_comment_topic:
                    break;
                case VKNotificationType.copy_post:
                    break;
                case VKNotificationType.copy_photo:
                    break;
                case VKNotificationType.copy_video:
                    break;
                case VKNotificationType.mention_comment_photo:
                    break;
                case VKNotificationType.mention_comment_video:
                    break;
            }

            return null;
        }
    }
}
