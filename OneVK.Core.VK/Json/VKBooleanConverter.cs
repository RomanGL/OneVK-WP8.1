using Newtonsoft.Json;
using System;

namespace OneVK.Core.VK.Json
{
    /// <summary>
    /// Представляет Json конвертер 1-0 то <see cref="bool"/>.
    /// </summary>
    internal sealed class VKBooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) { return objectType == typeof(bool); }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value.ToString() == "1";
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
        }
    }
}
