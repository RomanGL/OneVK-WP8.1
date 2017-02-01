using OneVK.Model.Notifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    public class SkipNumberUsersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var list = value as IList<IActionObjectContainer>;
            if (list == null) return null;
            int skip = int.Parse(parameter.ToString());

            if (skip == list.Count) return null;
            return list.Skip(skip);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
