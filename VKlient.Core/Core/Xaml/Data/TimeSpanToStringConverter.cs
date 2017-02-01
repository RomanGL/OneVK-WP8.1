using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OneVK.Core.Xaml.Data
{
    /// <summary>
    /// Конвертирует отрезок времени в его строковое представление.
    /// </summary>
    public class TimeSpanToStringConverter : IValueConverter
    {
        /// <summary>
        /// Конверитирует System.TimeSpan в его строковое представление.
        /// Поддерживает инверсию.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {            
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.GetType() != typeof(TimeSpan))
                throw new ArgumentException("targetType", "Ожидался тип System.TimeSpan.");

            bool isNegative = parameter == null ? 
                false : bool.Parse(parameter.ToString());
            var time = (TimeSpan)value;

            return isNegative ? 
                time.ToString(@"\-mm\:ss") : 
                time.ToString(@"mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
