using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.Model.Newsfeed
{
    /// <summary>
    /// Объект пользовательского спика новостей.
    /// </summary>
    public class VKNewsfeedList : IVKObject
    {
        /// <summary>
        /// Идентификатор пользовательского списка новостей.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Название пользовательского списка новостей.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
