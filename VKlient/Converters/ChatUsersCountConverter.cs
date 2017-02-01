using OneVK.Core.Messages;
using OneVK.Helpers;
using System;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует чат в строку количества участников.
    /// </summary>
    public class ChatUsersCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IChat == false) return String.Empty;

            var chat = (IChat)value;

            if (chat.Users == null || chat.Users.Count == 0) return "Нет участников";

            switch (CoreHelper.GetNumberMark(chat.Users.Count))
            {
                case 1: return chat.Users.Count + " участник";
                case 2: return chat.Users.Count + " участника";
                case 3: return chat.Users.Count + " участников";
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
