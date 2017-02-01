using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Enums.LongPoll;

namespace OneVK.Model.LongPoll
{
    /// <summary>
    /// Представляет объект с информацией об обновлении, 
    /// полученном от LongPoll-сервера ВКонтакте.
    /// </summary>
    public sealed class VKLongPollUpdate
    {
        private object _info;

        /// <summary>
        /// Возвращает тип обновления.
        /// </summary>
        public VKLongPollUpdateType Type { get; private set; }
        
        /// <summary>
        /// Возвращает объект с информацией о событии.
        /// </summary>
        public object GetInfo() { return _info; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным 
        /// массивом объектов информации.
        /// </summary>
        /// <param name="data">Массив объектов информации.</param>
        /// <exception cref="ArumentException"/>
        internal VKLongPollUpdate(object[] data)
        {
            if (data == null || data.Count() < 3)
                throw new ArgumentException("data",
                    "Массив объектов информации должен быть инициализирован и должен иметь как минимум 3 элемента.");

            Type = (VKLongPollUpdateType)((long)data[0]);

            switch (Type)
            {
                case VKLongPollUpdateType.MessageDeleted:
                    _info = new MessageDeletedInfo((ulong)((long)data[1]));
                    break;
                case VKLongPollUpdateType.MessageFlagsReplaced:
                    _info = new MessageFlagsInfo { MessageID = (ulong)((long)data[1]), Flags = (VKMessageFlags)((long)data[2]) };
                    break;
                case VKLongPollUpdateType.MessageFlagsSetted:
                    break;
                case VKLongPollUpdateType.MessageFlagsResetted:
                    break;
                case VKLongPollUpdateType.NewMessage:
                    long flags = (long)data[2];
                    long id = (long)data[3];                    

                    var msg = new MessageInfo
                    {
                        MessageID = (ulong)((long)data[1]),
                        Flags = (VKMessageFlags)((long)data[2]),
                        Timestamp = (new DateTime(1970, 1, 1, 0, 0, 0)).AddSeconds((long)data[4]),
                        Subject = (string)data[5],
                        Text = (string)data[6]
                    };

                    if (id - 2000000000 > 0) msg.ChatID = (uint)id - 2000000000;
                    else msg.UserID = (ulong)id;

                    if (flags - 8000 > 0) msg.Flags = (VKMessageFlags)flags - 8000;
                    else msg.Flags = (VKMessageFlags)flags;

                    _info = msg;
                    break;
                case VKLongPollUpdateType.ReceivedMessagesReaded:
                    break;
                case VKLongPollUpdateType.SentMessagesReaded:
                    break;
                case VKLongPollUpdateType.UserOnline:
                    break;
                case VKLongPollUpdateType.UserOffline:
                    break;
                case VKLongPollUpdateType.ChatParametersChanged:
                    break;
                case VKLongPollUpdateType.UserIsTypingInDialog:
                    _info = new UserIsTypingInfo((ulong)((long)data[1]));
                    break;
                case VKLongPollUpdateType.UserIsTypingInChat:
                    _info = new UserIsTypingInfo(
                        (ulong)((long)data[1]),
                        (uint)((long)data[2]));
                    break;
                case VKLongPollUpdateType.UserMakesACall:
                    break;
                case VKLongPollUpdateType.MessageCounterChanged:
                    _info = new MessagesCounterInfo((int)(long)data[1]);
                    break;
            }
        }
    }
}
