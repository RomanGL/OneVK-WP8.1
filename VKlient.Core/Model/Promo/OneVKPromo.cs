using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneVK.Model.Promo
{
    /// <summary>
    /// Представляет рекламную акцию OneVK.
    /// </summary>
    public class OneVKPromo
    {
        /// <summary>
        /// Заголовок рекламной акции.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Подзаголовок рекламной акции.
        /// </summary>
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        /// <summary>
        /// Изображение рекламной акции.
        /// </summary>
        [JsonProperty("promo_img")]
        public string PromoImage { get; set; }
                
        /// <summary>
        /// Описание рекламной акции.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Внешняя ссылка рекламной акции.
        /// </summary>
        [JsonProperty("external_link")]
        public string ExternalLink { get; set; }

        /// <summary>
        /// Название кнопки внешней ссылки.
        /// </summary>
        [JsonProperty("external_name")]
        public string ExternalName { get; set; }

        /// <summary>
        /// Идентификатор сообщества ВКонтакте.
        /// </summary>
        [JsonProperty("group_id")]
        public ulong GroupID { get; set; }
    }
}
