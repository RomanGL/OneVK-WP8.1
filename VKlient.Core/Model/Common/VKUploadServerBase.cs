using System;
using Newtonsoft.Json;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Универсальный объект сервера для выгрузки файлов ВКонтакте.
    /// </summary>
    public class VKUploadServerBase
    {
        /// <summary>
        /// Ссылка для выгрузки.
        /// </summary>
        [JsonProperty("upload_url")]
        public string UploadURL { get; set; }
    }
}
