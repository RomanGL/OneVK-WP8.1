using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneVK.Response.Bot
{
    /// <summary>
    /// Представляет ответ сервиса iii.ru.
    /// </summary>
    /// <typeparam name="T">Тип данных.</typeparam>
    internal class BotResponse<T>
    {
        [JsonProperty("result")]
        public T Response { get; set; }
    }
}
