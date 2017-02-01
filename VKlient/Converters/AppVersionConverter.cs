using System;
using Windows.UI.Xaml.Data;
using Windows.ApplicationModel;

namespace OneVK.Converters
{
    /// <summary>
    /// Возвращает версию пакета приложения.
    /// </summary>
    public class AppVersionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return "0.0.3.0";
            return Package.Current.Id.Version.ToString();            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
