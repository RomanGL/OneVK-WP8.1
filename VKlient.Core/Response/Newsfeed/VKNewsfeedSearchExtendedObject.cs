using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Model.Group;
using OneVK.Model.Profile;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой ответ сервера ВКонтакте
    /// на вызов метода newsfeed.search с расширенной информацией.
    /// </summary>
    public sealed class VKNewsfeedSearchExtendedObject : VKNewsfeedSearchObject
    {
        /// <summary>
        /// Список пользователей, упомянутых в результатах поиска.
        /// </summary>
        [JsonProperty("profiles")]
        List<VKProfileBase> Profiles { get; set; }

        /// <summary>
        /// Список сообществ, упомянутых в результатах поиска.
        /// </summary>
        [JsonProperty("groups")]
        List<VKGroupBase> Groups { get; set; }
    }
}
