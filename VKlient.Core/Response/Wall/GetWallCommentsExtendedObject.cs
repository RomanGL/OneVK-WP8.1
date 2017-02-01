using Newtonsoft.Json;
using OneVK.Model.Comment;
using OneVK.Model.Group;
using OneVK.Model.Profile;
using System.Collections.Generic;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервера на запрос wall.getComments с расширенной информацией.
    /// </summary>
    public class GetWallCommentsExtendedObject : VKCountedItemsObject<VKComment>
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
