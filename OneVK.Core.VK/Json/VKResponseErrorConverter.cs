using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using OneVK.Core.VK.Models.Common;

namespace OneVK.Core.VK.Json
{
    /// <summary>
    /// Представляет конвертер для ошибки запроса ВКонтакте.
    /// </summary>
    internal sealed class VKResponseErrorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) { return objectType == typeof(VKErrors); }
        public override bool CanWrite { get { return false; } }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            JToken typeToken;

            if (obj.TryGetValue("error_code", out typeToken))
            {
                return typeToken.ToObject<VKErrors>();
            }

            return VKErrors.None;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
