using Newtonsoft.Json;
using OneVK.Model.Group;
using OneVK.Model.Profile;
using System.Collections.Generic;

namespace OneVK.Response.Execute
{
    /// <summary>
    /// Представляет ответ сервиса ВКонтакте на запрос execute.getProfileInfo.
    /// </summary>
    public class ExecuteGetProfileInfoResponse
    {
        /// <summary>
        /// Профиль пользователя.
        /// </summary>        
        [JsonProperty("profile")]
        public VKProfileBaseExtended Profile { get; set; }
    }
}
