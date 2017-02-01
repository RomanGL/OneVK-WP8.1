using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер для корректного отображения надписи с количеством участников.
    /// </summary>
    public class GroupMembersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var membersCount = (long)value;

            if (membersCount > 21 || membersCount < 5)
            {
                long n = membersCount % 10;
                if (n == 1)
                    return membersCount + " участник";
                if (n > 1 && n <= 4)
                    return membersCount + " участника";
                else
                    return membersCount + " участников";
            }
            else
                return membersCount + " участников";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
