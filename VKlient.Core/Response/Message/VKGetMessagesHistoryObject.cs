using Newtonsoft.Json;
using OneVK.Model.Message;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ сервера ВКонтакте на запрос получения истории сообщений диалога или чата.
    /// </summary>
    public class VKGetMessagesHistoryObject : VKCountedItemsObject<VKMessage>
    {
        /// <summary>
        /// Количество непрочитанных сообщений.
        /// </summary>
        [JsonProperty("unread")]
        public uint Unread { get; set; }

        /// <summary>
        /// Количество пропущенных элементов (если применимо).
        /// </summary>
        [JsonProperty("skipped")]
        public uint Skipped { get; set; }
    }
}
