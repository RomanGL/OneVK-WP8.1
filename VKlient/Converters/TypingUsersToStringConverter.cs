using OneVK.Model.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер списка пишущих пользователей в строку.
    /// </summary>
    public class TypingUsersToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var users = value as ObservableCollection<KeyValuePair<DispatcherTimer, VKProfileChat>>;
            if (users == null) return "";

            if (users.Count == 0) return "";
            if (users.Count == 1) return String.Format("{0} набирает сообщение...", users[0].Value.FullName);

            return String.Format("{0} набирают сообщение...", String.Join(", ", 
                users.Select(p => String.Format("{0} {1}", p.Value.FirstName, p.Value.LastName[0])))); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
