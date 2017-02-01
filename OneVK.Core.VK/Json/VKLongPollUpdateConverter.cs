using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneVK.Core.Models.Json;
using OneVK.Core.VK.Models.LongPoll;
using System;
using System.Linq;

namespace OneVK.Core.VK.Json
{
    /// <summary>
    /// Представляет Json конвертер для LongPoll обновлений.
    /// </summary>
    internal sealed class VKLongPollUpdateConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType) { return objectType == typeof(IVKLongPollUpdate); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var tokens = JToken.ReadFrom(reader).ToList();
            var type = (VKLongPollUpdateType)tokens[0].Value<byte>();

            switch (type)
            {
                case VKLongPollUpdateType.MessageDeleted:
                    return new MessageDeletedUpdate { MessageID = tokens[1].Value<ulong>() };
                case VKLongPollUpdateType.MessageFlagsReplaced:
                    return new MessageFlagsReplacedUpdate
                    {
                        MessageID = tokens[1].Value<ulong>(),
                        Flags = (VKMessageFlags)tokens[2].Value<ushort>()
                    };
                case VKLongPollUpdateType.MessageFlagsSetted:
                    return new MessageFlagsSettedUpdate
                    {
                        MessageID = tokens[1].Value<ulong>(),
                        Mask = (VKMessageFlags)tokens[2].Value<ushort>(),
                        UserID = tokens.Count == 4 ? tokens[3].Value<long>() : 0
                    };
                case VKLongPollUpdateType.MessageFlagsResetted:
                    return new MessageFlagsResettedUpdate
                    {
                        MessageID = tokens[1].Value<ulong>(),
                        Mask = (VKMessageFlags)tokens[2].Value<ushort>(),
                        UserID = tokens.Count == 4 ? tokens[3].Value<long>() : 0
                    };
                case VKLongPollUpdateType.MessageReceived:
                    var received = new MessageReceivedUpdate
                    {
                        MessageID = tokens[1].Value<ulong>(),
                        Flags = (VKMessageFlags)tokens[2].Value<ushort>(),
                        Timestamp = JsonConvert.DeserializeObject<DateTime>(tokens[4].ToString(), new UnixtimeToDateTimeConverter()),
                        Subject = tokens[5].ToString(),
                        Text = tokens[6].ToString()
                    };

                    long fromID = tokens[3].Value<long>();
                    if (fromID > 2000000000)
                        received.ChatID = (uint)(fromID - 2000000000);
                    else received.UserID = fromID;

                    return received;
                case VKLongPollUpdateType.ReceivedMessagesReaded:
                    break;
                case VKLongPollUpdateType.SentMessagesReaded:
                    break;
                case VKLongPollUpdateType.UserOnline:
                    break;
                case VKLongPollUpdateType.UserOffline:
                    break;
                case VKLongPollUpdateType.FolderFilterFlagsResetted:
                    break;
                case VKLongPollUpdateType.FolderFilterFlagsReplaced:
                    break;
                case VKLongPollUpdateType.FolderFilterFlagsSetted:
                    break;
                case VKLongPollUpdateType.PeerMessagesFlagsReplaced:
                    break;
                case VKLongPollUpdateType.PeerMessagesFlagsSetted:
                    break;
                case VKLongPollUpdateType.PeerMessagesFlagsResetted:
                    break;
                case VKLongPollUpdateType.ChatParametersChanged:
                    break;
                case VKLongPollUpdateType.UserIsTypingInDialog:
                    return new UserIsTypingInDialogUpdate { UserID = tokens[1].Value<long>() };
                case VKLongPollUpdateType.UserIsTypingInChat:
                    return new UserIsTypingInChatUpdate
                    {
                        UserID = tokens[1].Value<long>(),
                        ChatID = tokens[2].Value<uint>()
                    };
                case VKLongPollUpdateType.UserMakesACall:
                    break;
                case VKLongPollUpdateType.MessagesCounterChanged:
                    return new MessagesCounterChangedUpdate { Number = tokens[1].Value<uint>() };
                case VKLongPollUpdateType.NotificationsSettingsChanged:
                    break;
            }

            var raw = new RawUpdate { Type = type };
            raw.UpdateInfo = new object[tokens.Count];

            for (int i = 0; i < tokens.Count; i++)
                raw.UpdateInfo[i] = tokens[i].Value<object>();

            return raw;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
