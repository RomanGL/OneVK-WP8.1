using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Model.Message;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ ВКонтакте на запрос диалогов.
    /// </summary>
    /// <typeparam name="T">Тип содержащихся в ответе объектов.</typeparam>
    public class VKGetDialogsObject : VKCountedItemsObject<VKDialog>
    {
        /// <summary>
        /// Количество диалогов с непрочитанными входящими сообщениями.
        /// </summary>
        [JsonProperty("unread_dialogs")]
        public int UnreadDialogs { get; set; }
    }
}
