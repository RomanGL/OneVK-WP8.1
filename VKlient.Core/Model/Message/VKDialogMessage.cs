using Newtonsoft.Json;

namespace OneVK.Model.Message
{
    /// <summary>
    /// Представляет сообщение из списка диалогов.
    /// </summary>
    public class VKDialogMessage : VKMessage
    {        
        /// <summary>
        /// Заголовок сообщения или беседы.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Фотография беседы в размере 50 px.
        /// </summary>
        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
    }
}
