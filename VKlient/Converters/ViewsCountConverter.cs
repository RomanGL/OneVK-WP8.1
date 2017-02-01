using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер для корректного отображения надписи с количеством просмотров.
    /// </summary>
    public class ViewsCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var membersCount = (long)value;

            if (membersCount > 21 || membersCount < 5)
            {
                long n = membersCount % 10;
                if (n == 1)
                    return membersCount + " просмотр";
                if (n > 1 && n <= 4)
                    return membersCount + " просмотра";
                else
                    return membersCount + " просмотров";
            }
            else
                return membersCount + " просмотров";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
