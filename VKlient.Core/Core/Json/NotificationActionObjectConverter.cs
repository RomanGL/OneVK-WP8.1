using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneVK.Enums.Notifications;
using OneVK.Model.Notifications;
using System;

namespace OneVK.Core.Json
{
    /// <summary>
    /// Представляет конвертер для типа <see cref="INotificationActionObject"/>.
    /// </summary>
    public class NotificationActionObjectConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType) { return objectType == typeof(INotificationActionObject); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            ActionObjectType type = token["type"].ToObject<ActionObjectType>();

            switch (type)
            {
                case ActionObjectType.User:
                    return token.ToObject<VKNotificationProfile>();
                case ActionObjectType.Group:
                    return token.ToObject<VKNotificationGroup>();
            }

            throw new InvalidOperationException("Не удалось десериализовать объект-инициатор оповещения.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
