using Newtonsoft.Json;
using System.Collections.Generic;
using OneVK.Enums.Common;

namespace OneVK.Model.Common
{
    /// <summary>
    /// Настройки приватности в специальном формате.
    /// </summary>
    public sealed class VKPrivacy
    {
        /// <summary>
        /// Тип приватности.
        /// </summary>
        [JsonProperty("type")]
        public VKPrivacyType Type { get; set; }
        /// <summary>
        /// Массив из пользовательских списков, которые добавляются к пользователям, принадлежащим к типу в поле type.
        /// </summary>
        [JsonProperty("lists")]
        public List<int> Lists { get; set; }
        /// <summary>
        /// Массив из пользовательских списков, которые исключаются из пользователей, принадлежащим к типу в поле type.
        /// </summary>
        [JsonProperty("except_lists")]
        public List<int> ExceptLists { get; set; }
        /// <summary>
        /// Массив из идентификаторов пользователей, которые добавляются к пользователям, принадлежащим к типу в поле type.
        /// </summary>
        [JsonProperty("users")]
        public List<long> Users { get; set; }
        /// <summary>
        /// Массив из идентификаторов пользователей, которые исключаются из пользователей , принадлежащим к типу в поле type.
        /// </summary>
        [JsonProperty("except_users")]
        public List<long> ExceptUsers { get; set; }
    }
}
