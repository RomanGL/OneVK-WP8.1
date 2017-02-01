using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Представляет конвертер времени для сообщений.
    /// </summary>
    public class MessagesTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var time = (DateTime)value;
            time = time.ToLocalTime();

            if (time.DayOfYear == DateTime.Now.DayOfYear)
                return time.ToString("HH:mm");
            else if (time.Year == DateTime.Now.Year)
                return time.ToString("dd.MM, HH:mm");
            else
                return time.ToString("dd.MM,yyy, HH:mm");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
