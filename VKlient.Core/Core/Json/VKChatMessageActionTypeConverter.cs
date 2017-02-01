using Newtonsoft.Json;
using OneVK.Model.Message;
using System;

namespace OneVK.Core.Json
{
    /// <summary>
    /// Представляет конвертер для типа <see cref="VKChatMessageActionType"/>.
    /// </summary>
    public class VKChatMessageActionTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(VKChatMessageActionType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value.ToString())
            {
                case "chat_photo_update": return VKChatMessageActionType.ChatPhotoUpdate;
                case "chat_photo_remove": return VKChatMessageActionType.ChatPhotoRemove;
                case "chat_create": return VKChatMessageActionType.ChatCreate;
                case "chat_title_update": return VKChatMessageActionType.ChatTitleUpdate;
                case "chat_invite_user": return VKChatMessageActionType.ChatInviteUser;
                case "chat_kick_user": return VKChatMessageActionType.ChatKickUser;
                default: return VKChatMessageActionType.None;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            switch ((VKChatMessageActionType)value)
            {
                case VKChatMessageActionType.ChatPhotoUpdate:
                    writer.WriteValue("chat_photo_update");
                    break;
                case VKChatMessageActionType.ChatPhotoRemove:
                    writer.WriteValue("chat_photo_remove");
                    break;
                case VKChatMessageActionType.ChatCreate:
                    writer.WriteValue("chat_create");
                    break;
                case VKChatMessageActionType.ChatTitleUpdate:
                    writer.WriteValue("chat_title_update");
                    break;
                case VKChatMessageActionType.ChatInviteUser:
                    writer.WriteValue("chat_invite_user");
                    break;
                case VKChatMessageActionType.ChatKickUser:
                    writer.WriteValue("chat_kick_user");
                    break;
                default:
                    writer.WriteValue("none");
                    break;
            }
        }
    }
}
