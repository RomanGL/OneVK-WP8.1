using OneVK.Enums.App;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует <see cref="ContentState"/> в состояние видимости.
    /// </summary>
    public class ContentStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ContentState == false) return Visibility.Visible;
            var state = (ContentState)value;

            if (state == ContentState.Normal) return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
