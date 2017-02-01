using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Group;
using OneVK.Model.Profile;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет собой ответ сервера ВКонтакте
    /// на вызов метода newsfeed.getBanned с расширенной информацией.
    /// </summary>
    public sealed class VKNewsfeedGetBannedExtendedObject
    {
        /// <summary>
        /// Информация о сообществах, которые пользователь
        /// скрыл из ленты новостей.
        /// </summary>
        [JsonProperty("groups")]
        public List<VKGroupBase> Groups { get; set; }

        /// <summary>
        /// Информация о пользователях, которые пользователь
        /// скрыл из ленты новостей.
        /// </summary>
        [JsonProperty("profiles")]
        public List<VKProfileBase> Profiles { get; set; }
    }
}
