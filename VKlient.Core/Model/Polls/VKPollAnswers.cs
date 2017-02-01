using Newtonsoft.Json;
using System;

namespace OneVK.Model.Polls
{
    /// <summary>
    /// Массив, содержащий объекты с вариантами ответа на вопрос в опросе.
    /// </summary>
    public sealed class VKPollAnswers : IVKObject
    {
        /// <summary>
        /// Идентификатор ответа на вопрос.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Текст ответа на вопрос.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Количество пользователей, проголосовавших за данный вариант ответа.
        /// </summary>
        [JsonProperty("votes")]
        public int Votes { get; set; }

        /// <summary>
        /// Рейтинг данного варинта ответа, выраженный в процентах.
        /// </summary>
        [JsonProperty("rate")]
        public double Rate { get; set; }
    }
}
