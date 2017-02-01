using OneVK.Enums.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер Boolean в перечисление видимости элемента управления.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Конвертирует Boolean в Visibility.
        /// </summary>
        /// <returns>Значение Visibility.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is VKBoolean)
            {
                var val = (VKBoolean)value;
                return val == VKBoolean.True ? Visibility.Visible : Visibility.Collapsed;
            }

            bool reverse = false;
            if (parameter != null) bool.TryParse(parameter.ToString(), out reverse);

            var flag = bool.Parse(value.ToString());

            if (reverse) return flag ? Visibility.Collapsed : Visibility.Visible;
            else return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Выполняет обратную конвертацию Visibility в Boolean.
        /// </summary>
        /// <returns>Значение Boolean.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var obj = (Visibility)value;

            if (obj == Visibility.Visible) return true;
            else return false;
        }
    }
}
