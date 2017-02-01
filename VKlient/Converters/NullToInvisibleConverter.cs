 using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Null to visible converter.
    /// </summary>
    public class NullToInvisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return Visibility.Collapsed;
            var ic = value as ICollection;
            if (ic != null && ic.Count == 0) return Visibility.Collapsed;
            
            if (value is string && String.IsNullOrEmpty(value.ToString())) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
