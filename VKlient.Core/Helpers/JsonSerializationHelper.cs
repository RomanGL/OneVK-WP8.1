using Newtonsoft.Json;
using System.IO;

namespace OneVK.Helpers
{
    /// <summary>
    /// Помощник сериализации в Json-строку.
    /// </summary>
#if PLAYER_TASK
    internal static class JsonSerializationHelper
#else
    public static class JsonSerializationHelper
#endif
    {
        /// <summary>
        /// Сериализует указанный объект в Json-строку.
        /// </summary>
        /// <param name="obj">Объект для сериализации.</param>
        public static string SerializeToJson<T>(T obj)
        {
            using (var writter = new StringWriter())
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writter, obj);
                return writter.GetStringBuilder().ToString();
            }  
        }

        /// <summary>
        /// Десериализует Json-строку в объект указанного типа.
        /// </summary>
        /// <param name="json">Json-строка для десериализации.</param>
        public static T DeserializeFromJson<T>(string json)
        {
            using (var reader = new StringReader(json))
            {
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var serializer = new JsonSerializer();
                    var result = serializer.Deserialize<T>(jsonReader);
                    return result;
                }
            }
        }
    }
}
