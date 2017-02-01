using Newtonsoft.Json;
using OneVK.Core.Collections;
using OneVK.Core.Json;
using OneVK.Enums.App;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Представляет собой беседу.
    /// </summary>
    [JsonConverter(typeof(JsonConversationsConverter))]
    public interface IConversation
    {
        /// <summary>
        /// Тип беседы.
        /// </summary>
        ConversationType Type { get; }
        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        long ID { get; set; }

        /// <summary>
        /// Заголовок беседы.
        /// </summary>
        string Title { get; set; }        
        /// <summary>
        /// Количество непрочитанных сообщений.
        /// </summary>
        uint UnreadNumber { get; set; }
        
        /// <summary>
        /// Коллекция сообщений беседы.
        /// </summary>
        MessagesCollection Messages { get; set; }
    }
}
