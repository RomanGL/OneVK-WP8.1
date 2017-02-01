using OneVK.Enums.App;
using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует <see cref="EmojiType"/> в строковое представление.
    /// </summary>
    public class EmojiTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is EmojiType == false) return value;

            switch ((EmojiType)value)
            {
                case EmojiType.Twitter:
                    return "Twitter Emoji";
                case EmojiType.Apple:
                    return "Apple Emoji";
                case EmojiType.Segoe:
                    return "Microsoft Segoe Emoji";
                default:
                    return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
