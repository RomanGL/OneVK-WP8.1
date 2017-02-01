using OneVK.Controls.Common;
using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует <see cref="bool"/> в <see cref="ChatBubbleDirection"/>.
    /// </summary>
    public class BooleanToChatBubbleDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool flag = bool.Parse(value.ToString());
            string normalDirection = parameter.ToString();

            if (flag) return ChatBubbleDirection.NoDirection;

            switch (normalDirection)
            {
                case "-1": return ChatBubbleDirection.Left;
                case "1": return ChatBubbleDirection.Right;
                default: return ChatBubbleDirection.NoDirection;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
