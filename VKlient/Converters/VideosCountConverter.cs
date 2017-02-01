using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер для корректного отображения надписи с количеством видеозаписей.
    /// </summary>
    public class VideosCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var count = (int)value;

            if (count > 21 || count < 5)
            {
                long n = count % 10;
                if (n == 1)
                    return count + " видеозапись";
                if (n > 1 && n <= 4)
                    return count + " видеозаписи";
                else
                    return count + " видеозаписей";
            }
            else
                return count + " видеозаписей";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
