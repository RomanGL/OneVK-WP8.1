using Newtonsoft.Json;
using System;
using OneVK.Core.LF.Models;

namespace OneVK.Core.LF.Json
{ 
    /// <summary>
    /// Представляет конвертер для типа <see cref="LFImageSize"/>.
    /// </summary>
    internal class LFImageSizeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LFImageSize);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.Value.ToString())
            {
                case "small": return LFImageSize.Small;
                case "medium": return LFImageSize.Medium;
                case "large": return LFImageSize.Large;
                case "extralarge": return LFImageSize.ExtraLarge;
                case "mega": return LFImageSize.Mega;
                default: return LFImageSize.Unknown;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
