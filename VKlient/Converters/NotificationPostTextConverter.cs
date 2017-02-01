using OneVK.Model.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер текста поста для оповещения.
    /// </summary>
    public class NotificationPostTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is VKNotificationPost == false) return String.Empty;

            var post = value as VKNotificationPost;
            if (String.IsNullOrWhiteSpace(post.Text) && post.CopyHistory != null)
                return post.CopyHistory[0].Text;
            else
                return post.Text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
