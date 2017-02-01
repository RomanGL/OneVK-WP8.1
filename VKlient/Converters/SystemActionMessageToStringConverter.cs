using OneVK.Enums.Profile;
using OneVK.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OneVK.Converters
{
    /// <summary>
    /// Конвертирует системное сообщение чата ВКонтакте в строку.
    /// </summary>
    public class SystemActionMessageToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var msg = value as VKMessage;
            if (msg == null) return value;

            switch (msg.Action)
            {
                case VKChatMessageActionType.ChatPhotoUpdate:
                    return "Фотография беседы обновлена";
                case VKChatMessageActionType.ChatPhotoRemove:
                    return "Фотография беседы удалена";
                case VKChatMessageActionType.ChatCreate:
                    return "Беседа \"" + msg.ActionText + "\" создана";
                case VKChatMessageActionType.ChatTitleUpdate:
                    return "Новое название беседы \"" + msg.ActionText + "\"";
                case VKChatMessageActionType.ChatInviteUser:
                    if ((long)msg.Sender.ID == msg.ActionMid)
                    {
                        return String.Format("{0} верну{1} в беседу",
                            msg.Sender.FullName,
                            msg.Sender.Sex == VKUserSex.Female ? "лась" : "лся");
                    }
                    return String.Format("{0} пригласил{1} в беседу {2}",
                            msg.Sender.FullName,
                            msg.Sender.Sex == VKUserSex.Female ? "а" : "",
                            msg.ActionMid > 0 ? msg.ActionUser.FullName : msg.ActionEmail);
                case VKChatMessageActionType.ChatKickUser:
                    if ((long)msg.Sender.ID == msg.ActionMid)
                    {
                        return String.Format("{0} покинул{1} беседу",
                            msg.Sender.FullName,
                            msg.Sender.Sex == VKUserSex.Female ? "а" : "");
                    }
                    return String.Format("{0} исключил{1} из беседы {2}",
                            msg.Sender.FullName,
                            msg.Sender.Sex == VKUserSex.Female ? "а" : "",
                            msg.ActionMid > 0 ? msg.ActionUser.FullName : msg.ActionEmail);
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
