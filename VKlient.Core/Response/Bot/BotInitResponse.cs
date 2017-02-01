using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneVK.Response.Bot
{
    /// <summary>
    /// Представляет ответ сервиса iii.ru на запрос инициализации.
    /// </summary>
    internal class BotInitResponse
    {
        [JsonProperty("cuid")]
        public string Cuid { get; set; }
    }
}
