using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneVK.Core.Collections;
using OneVK.Core.Messages;
using OneVK.Enums.App;
using OneVK.Model.Message;
using OneVK.Model.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Core.Json
{
    public sealed class JsonConversationsConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IConversation);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var type = obj["Type"].ToObject<ConversationType>();

            IConversation conversation;
            if (type == ConversationType.Dialog)
            {
                var dialog = new Dialog();
                dialog.UserID = obj["UserID"].ToObject<ulong>();
                dialog.User = obj["User"].ToObject<VKProfileShort>();
                dialog.Messages = new MessagesCollection(dialog.UserID,
                    obj["Messages"].ToObject<List<VKMessage>>());
                conversation = dialog;
            }
            else
            {
                var chat = new Chat();
                chat.ChatID = obj["ChatID"].ToObject<uint>();
                chat.AdminID = obj["AdminID"].ToObject<ulong>();
                chat.Users = obj["Users"].ToObject<ObservableCollection<VKProfileChat>>();
                chat.Messages = new MessagesCollection(chat.ChatID,
                    obj["Messages"].ToObject<List<VKMessage>>());
                conversation = chat;
            }

            conversation.Title = obj["Title"].ToString();
            conversation.UnreadNumber = obj["UnreadNumber"].ToObject<uint>();

            return conversation;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
