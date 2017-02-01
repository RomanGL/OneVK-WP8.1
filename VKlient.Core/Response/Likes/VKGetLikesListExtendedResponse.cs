using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на расширенный запрос списка лайкнувших.
    /// </summary>
    public class VKGetLikesListExtendedResponse
    {
        /// <summary>
        /// Сокращенный профиль пользователя.
        /// </summary>
        public class ShortProfile
        {
            /// <summary>
            /// Идентификатор пользователя.
            /// </summary>
            [JsonProperty("id")]
            public long ID { get; set; }

            /// <summary>
            /// Имя пользователя.
            /// </summary>
            [JsonProperty("first_name")]
            public string FirstName { get; set; }

            /// <summary>
            /// Фамилия пользователя.
            /// </summary>
            [JsonProperty("last_name")]
            public string LastName { get; set; }

            private ShortProfile() { }
        }

        /// <summary>
        /// Количество элементов.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>
        /// Коллекция элементов.
        /// </summary>
        [JsonProperty("items")]
        public List<ShortProfile> Items { get; set; } 
    }
}
