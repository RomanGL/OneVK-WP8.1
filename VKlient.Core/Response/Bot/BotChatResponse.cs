using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneVK.Response.Bot
{
    /// <summary>
    /// Представляет ответ сервиса iii.ru на запрос чата.
    /// </summary>
    internal class BotChatResponse
    {
        /// <summary>
        /// Информация об ответе бота.
        /// </summary>
        public class TextData
        {
            [JsonProperty("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// Текст ответа бота.
        /// </summary>
        [JsonProperty("text")]
        public TextData Text { get; set; }
    }
}
