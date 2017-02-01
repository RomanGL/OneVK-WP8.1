using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конверт, увеличивающий переданное значение на переданное в параметре.
    /// </summary>
    public class IncrementNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter == null) return value;
            return (int)value + int.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
