using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
using OneVK.Model.Photo;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер высоты изображения для новостной ленты.
    /// </summary>
    public class ImageWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //var image = value as VKPhoto;
            //if (image == null) return null;

            //if (image.IsLandscape)
            //    return null;
            //else
            //    return 300 * image.WidthToHeightRatio;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
