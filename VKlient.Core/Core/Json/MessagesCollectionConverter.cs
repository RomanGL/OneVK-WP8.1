using Newtonsoft.Json;
using OneVK.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using OneVK.Core.Collections;

namespace OneVK.Core.Json
{
    /// <summary>
    /// Представляет коннвертер для <see cref="MessagesCollection"/>
    /// </summary>
    public sealed class MessagesCollectionConverter : JsonConverter
    {
        /// <summary>
        /// Может ли конвертер выполнять чтение.
        /// </summary>
        public override bool CanRead { get { return false; } }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var collection = value as IEnumerable<VKMessage>;
            writer.WriteRawValue(JsonConvert.SerializeObject(collection.Take(15)));
        }
    }
}
