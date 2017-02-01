using GalaSoft.MvvmLight;
using System;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертер, возвращающий ширину экрана.
    /// </summary>
    public class ScreenWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (ViewModelBase.IsInDesignModeStatic) return 400;
            return Window.Current.Bounds.Width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
