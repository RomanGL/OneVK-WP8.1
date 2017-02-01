using Newtonsoft.Json;
using OneVK.Model.Group;
using OneVK.Model.Profile;
using OneVK.Model.Wall;
using System.Collections.Generic;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервера на запрос wall.get 
    /// с расширенной информацией.
    /// </summary>
    public class GetWallExtendedObject : VKCountedItemsObject<VKWallPost>
    {
        /// <summary>
        /// Пользователи, упоминавшиеся в ответе на запрос.
        /// </summary>
        [JsonProperty("profiles")]
        public List<VKProfileBase> Profiles { get; set; }

        /// <summary>
        /// Сообщетсва, упоминавшиеся в ответе на запрос.
        /// </summary>
        [JsonProperty("groups")]
        public List<VKGroupBase> Groups { get; set; }
    }
}
