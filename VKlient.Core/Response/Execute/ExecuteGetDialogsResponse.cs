using Newtonsoft.Json;
using OneVK.Model.Profile;
using System.Collections.Generic;

namespace OneVK.Response.Execute
{
    /// <summary>
    /// Представляет ответ сервиса ВКонтакте на запрос execute.getDialogs.
    /// </summary>
    public sealed class ExecuteGetDialogsResponse
    {
        /// <summary>
        /// Список диалогов.
        /// </summary>
        [JsonProperty("dialogs")]
        public VKGetDialogsObject Dialogs { get; set; }

        /// <summary>
        /// Список пользователей, с которыми ведутся диалоги.
        /// </summary>
        [JsonProperty("users")]
        public List<VKProfileChat> Users { get; set; }
    }
}
