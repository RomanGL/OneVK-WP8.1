using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Comment;
using OneVK.Model.Group;
using OneVK.Model.Profile;

namespace OneVK.Response
{
    /// <summary>
    /// Комментарий ВКонтакте с расширенными данными.
    /// </summary>
    public class VKCommentExtendedObject : VKCountedItemsObject<VKComment>
    {       
        /// <summary>
        /// Список пользователей.
        /// </summary>
        [JsonProperty("profiles")]
        public List<VKProfileBase> Profiles { get; set; }

        /// <summary>
        /// Список сообществ.
        /// </summary>
        [JsonProperty("groups")]
        public List<VKGroupBase> Groups { get; set; }
    }
}
