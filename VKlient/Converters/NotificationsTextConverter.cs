using OneVK.Enums.Notifications;
using OneVK.Enums.Profile;
using OneVK.Helpers;
using OneVK.Model.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    public class NotificationsTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var notification = value as IVKNotification;

            switch (notification.Type)
            {
                case VKNotificationType.follow:
                    return GetFollowText((VKFollowNotification)notification);
                case VKNotificationType.friend_accepted:
                    return GetFriendAcceptedText((VKFriendAcceptedNotification)notification);
                case VKNotificationType.mention:
                    break;
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
                case VKNotificationType.reply_comment:
                    break;
                case VKNotificationType.reply_comment_photo:
                    break;
                case VKNotificationType.reply_comment_video:
                    break;
                case VKNotificationType.reply_topic:
                    break;
                case VKNotificationType.like_post:
                    return GetLikePostText((VKLikePostNotification)notification);
                case VKNotificationType.like_comment:
                    return GetLikeCommentText((VKLikeCommentNotification)notification);
                case VKNotificationType.like_photo:
                    return GetLikePhotoText((VKLikePhotoNotification)notification);
                case VKNotificationType.like_video:
                    return GetLikeVideoText((VKLikeVideoNotification)notification);
                case VKNotificationType.like_comment_photo:
                    return GetLikeCommentText((VKLikeCommentNotification)notification);
                case VKNotificationType.like_comment_video:
                    return GetLikeCommentText((VKLikeCommentNotification)notification);
                case VKNotificationType.like_comment_topic:
                    return GetLikeCommentText((VKLikeCommentNotification)notification);
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
                default:
                    break;
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private static string GetLikePostText(VKLikePostNotification notification)
        {
            var firstUser = (VKNotificationProfile)notification.Feedback.Items.First().ActionObject;

            if (notification.Feedback.Items.Count == 1)
                return "оценил" + GetLastWordSymbol(firstUser.Sex) + " вашу запись";

            string result = "и еще " + (notification.Feedback.Items.Count - 1);
            switch (CoreHelper.GetNumberMark(notification.Feedback.Items.Count - 1))
            {
                case 1:
                    result += " пользователь оценили вашу запись";
                    break;
                case 2:
                    result += " пользователя оценили вашу запись";
                    break;
                case 3:
                    result += " пользователей оценили вашу запись";
                    break;
            }

            return result;
        }

        private static string GetLikeCommentText(VKLikeCommentNotification notification)
        {
            var firstUser = (VKNotificationProfile)notification.Feedback.Items.First().ActionObject;

            if (notification.Feedback.Items.Count == 1)
                return "оценил" + GetLastWordSymbol(firstUser.Sex) + " ваш комментарий";

            string result = "и еще " + (notification.Feedback.Items.Count - 1);
            switch (CoreHelper.GetNumberMark(notification.Feedback.Items.Count - 1))
            {
                case 1:
                    result += " пользователь оценили ваш комментарий";
                    break;
                case 2:
                    result += " пользователя оценили ваш комментарий";
                    break;
                case 3:
                    result += " пользователей оценили ваш комментарий";
                    break;
            }

            return result;
        }

        private static string GetFriendAcceptedText(VKFriendAcceptedNotification notification)
        {
            var firstUser = (VKNotificationProfile)notification.Feedback.Items.First().ActionObject;

            if (notification.Feedback.Items.Count == 1)
                return "принял" + GetLastWordSymbol(firstUser.Sex) + " вашу заявку в друзья";

            string result = "и еще " + (notification.Feedback.Items.Count - 1);
            switch (CoreHelper.GetNumberMark(notification.Feedback.Items.Count - 1))
            {
                case 1:
                    result += " пользователь приняли ваши заявки в друзья";
                    break;
                case 2:
                    result += " пользователя приняли ваши заявки в друзья";
                    break;
                case 3:
                    result += " пользователей приняли ваши заявки в друзья";
                    break;
            }

            return result;
        }

        private static string GetFollowText(VKFollowNotification notification)
        {
            var firstUser = (VKNotificationProfile)notification.Feedback.Items.First().ActionObject;

            if (notification.Feedback.Items.Count == 1)
                return "подписал" + GetLastWordSymbol(firstUser.Sex, true) + " на ваши обновления";

            string result = "и еще " + (notification.Feedback.Items.Count - 1);
            switch (CoreHelper.GetNumberMark(notification.Feedback.Items.Count - 1))
            {
                case 1:
                    result += " пользователь подписались на ваши обновления";
                    break;
                case 2:
                    result += " пользователя подписались на ваши обновления";
                    break;
                case 3:
                    result += " пользователей подписались на ваши обновления";
                    break;
            }

            return result;
        }

        private static string GetLikePhotoText(VKLikePhotoNotification notification)
        {
            var firstUser = (VKNotificationProfile)notification.Feedback.Items.First().ActionObject;

            if (notification.Feedback.Items.Count == 1)
                return "оценил" + GetLastWordSymbol(firstUser.Sex) + " вашу фотографию";

            string result = "и еще " + (notification.Feedback.Items.Count - 1);
            switch (CoreHelper.GetNumberMark(notification.Feedback.Items.Count - 1))
            {
                case 1:
                    result += " пользователь оценили вашу фотографию";
                    break;
                case 2:
                    result += " пользователя оценили вашу фотографию";
                    break;
                case 3:
                    result += " пользователей оценили вашу фотографию";
                    break;
            }

            return result;
        }

        private static string GetLikeVideoText(VKLikeVideoNotification notification)
        {
            var firstUser = (VKNotificationProfile)notification.Feedback.Items.First().ActionObject;

            if (notification.Feedback.Items.Count == 1)
                return "оценил" + GetLastWordSymbol(firstUser.Sex) + " вашу видеозапись";

            string result = "и еще " + (notification.Feedback.Items.Count - 1);
            switch (CoreHelper.GetNumberMark(notification.Feedback.Items.Count - 1))
            {
                case 1:
                    result += " пользователь оценили вашу видеозапись";
                    break;
                case 2:
                    result += " пользователя оценили вашу видеозапись";
                    break;
                case 3:
                    result += " пользователей оценили вашу видеозапись";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Возвращает символ-окончание слова в зависимости от пола пользователя.
        /// </summary>
        /// <param name="sex">Пол пользователя.</param>
        /// <param name="selfEnd">Окончание -ась/-ся</param>
        private static string GetLastWordSymbol(VKUserSex sex, bool selfEnd = false)
        {
            if (selfEnd)
            {
                if (sex == VKUserSex.Female) return "ась";
                return "ся";
            }
            else
            {
                if (sex == VKUserSex.Female) return "а";
            }
            return String.Empty;
        }
    }
}
