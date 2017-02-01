using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneVK.Core.VK.Models.Groups;
using System;

namespace OneVK.Core.VK.Json
{
    /// <summary>
    /// Конвертирует счетчики сообществ.
    /// </summary>
    internal sealed class VKGroupCountersConverter : JsonConverter
    {
        public override bool CanWrite { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(VKGroupCounters);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            if (token is JArray)
                return new VKGroupCounters();

            return token.ToObject<VKGroupCounters>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
