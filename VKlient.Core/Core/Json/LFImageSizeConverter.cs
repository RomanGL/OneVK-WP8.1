using Newtonsoft.Json;
using OneVK.Enums.Common;
using System;

namespace OneVK.Core.Json
{
    /// <summary>
    /// Представляет конвертер для типа <see cref="LFImageSize"/>.
    /// </summary>
    public class LFImageSizeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LFImageSize);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value.ToString())
            {
                case "small": return LFImageSize.small;
                case "medium": return LFImageSize.medium;
                case "large": return LFImageSize.large;
                case "extralarge": return LFImageSize.extralarge;
                case "mega": return LFImageSize.mega;
                default: return LFImageSize.unknown;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
