using System;
using Windows.UI.Xaml.Data;
using OneVK.Enums.Message;
using Windows.UI.Xaml;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует <see cref="VKMessageType" в <see cref="Visibility"/>./>
    /// </summary>
    public class MessageTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is VKMessageType == false) return Visibility.Collapsed;
            var type = (VKMessageType)value;

            return type == VKMessageType.Received ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
