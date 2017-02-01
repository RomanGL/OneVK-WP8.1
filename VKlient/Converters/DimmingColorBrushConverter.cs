using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер для цветов. Делает кисть более темной.
    /// </summary>
    public class DimmingColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var brush = (SolidColorBrush)value;
            byte k = byte.Parse(parameter.ToString());
            Color color = brush.Color;

            int res = color.R - k;
            color.R = res < 0 ? (byte)0 : (byte)res;
            res = color.G - k;
            color.G = res < 0 ? (byte)0 : (byte)res;
            res = color.B - k;
            color.B = res < 0 ? (byte)0 : (byte)res;

            brush.Color = color;
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
