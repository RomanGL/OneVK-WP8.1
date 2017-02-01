using Newtonsoft.Json;
using System;
using OneVK.Core.Json;
using OneVK.Enums.Gifts;

namespace OneVK.Model.Gifts
{
    /// <summary>
    /// Представляет подарок ВКонтакте.
    /// </summary>
    public class VKGiftItem : IVKObject
    {
        /// <summary>
        /// Идентификатор полученного подарка.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Идентификатор пользователя, который отправил подарок.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; }

        /// <summary>
        /// Текст сообщения, приложенного к подарку.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Время отправки подарка.
        /// </summary>
        [JsonProperty("date")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Объект подарка.
        /// </summary>
        [JsonProperty("gift")]
        public VKGift Gift { get; set; }

        /// <summary>
        /// Значение приватности подарка.
        /// </summary>
        [JsonProperty("privacy")]
        public VKGiftsPrivacy Privacy { get; set; }
    }
}
