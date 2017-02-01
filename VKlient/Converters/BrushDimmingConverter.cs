using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер затемнения для <see cref="SolidColorBrush"/>.
    /// </summary>
    public class BrushDimmingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var brush = value as SolidColorBrush;
            if (brush == null) return value;

            byte k = 0;
            if (parameter != null) byte.TryParse(parameter.ToString(), out k);

            var color = new Color()
            {
                A = brush.Color.A,
                R = (byte)(brush.Color.R - k),
                G = (byte)(brush.Color.B - k),
                B = (byte)(brush.Color.B - k)                
            };

            return new SolidColorBrush(color) { Opacity = brush.Opacity, RelativeTransform = brush.RelativeTransform, Transform = brush.Transform };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
