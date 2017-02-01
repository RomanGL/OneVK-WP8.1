using System;
using OneVK.Model.Photo;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер высоты изображения в новостной ленте.
    /// </summary>
    public class ImageHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //var image = value as VKPhoto;
            //if (image == null) return null;

            //if (image.IsLandscape)
            //    return Window.Current.Bounds.Width / image.WidthToHeightRatio;
            //else
            //    return 300;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
