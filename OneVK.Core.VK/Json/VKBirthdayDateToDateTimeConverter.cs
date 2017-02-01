using Newtonsoft.Json;
using System;

namespace OneVK.Core.VK.Json
{
    /// <summary>
    /// Конвертирует дату дня рождения ВКонтакте в <see cref="DateTime"/>.
    /// </summary>
    public sealed class VKBirthdayDateToDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            string date = reader.Value.ToString();

            DateTime result;
            if (DateTime.TryParse(date, out result))
                return result;
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteValue(((DateTime?)value).Value.ToString("dd.MM.yyyy"));
        }
    }
}
