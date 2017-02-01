using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует <see cref="null"/> в <see cref="bool"/>.
    /// </summary>
    public sealed class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool reverse = false;
            if (parameter != null)
                bool.TryParse(parameter.ToString(), out reverse);

            return reverse ? value != null : value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
