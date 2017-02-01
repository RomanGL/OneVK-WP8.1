using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертер видимости счетчика.
    /// </summary>
    public class CounterVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            uint count = uint.Parse(value.ToString());
            bool reverse = false;
            if (parameter != null)
                bool.TryParse(parameter.ToString(), out reverse);

            if (reverse) return count > 0 ? Visibility.Collapsed : Visibility.Visible;
            return count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
