using Newtonsoft.Json;
using OneVK.Model.Message;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос удаления фотографии из мультидиалога.
    /// </summary>
    public class VKChatPhotoObject
    {
        /// <summary>
        /// Количество диалогов с непрочитанными входящими сообщениями.
        /// </summary>
        [JsonProperty("message_id")]
        public long MessageID { get; set; }

        /// <summary>
        /// Объект мультидиалога.
        /// </summary>
        [JsonProperty("chat")]
        public VKChat Chat { get; set; }
    }
}
