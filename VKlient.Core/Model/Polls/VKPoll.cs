using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using OneVK.Core.Json;
using OneVK.Enums.Common;

namespace OneVK.Model.Polls
{
    /// <summary>
    /// Представляет опрос ВКонтакте.
    /// </summary>
    public class VKPoll
    {
        /// <summary>
        /// Идентификатор владельца опроса.
        /// </summary>
        [JsonProperty("owner_id")]
        public long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор опроса.
        /// </summary>
        [JsonProperty("id")]
        public long ID { get; set; }

        /// <summary>
        /// Время создания опроса.
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(UnixtimeToDateTimeConverter))]
        public DateTime Created { get; set; }

        /// <summary>
        /// Текст вопроса для опроса.
        /// </summary>
        [JsonProperty("question")]
        public string Question { get; set; }

        /// <summary>
        /// Общее количество ответивших пользователей.
        /// </summary>
        [JsonProperty("votes")]
        public int Votes { get; set; }

        /// <summary>
        /// Идентификатор ответа текущего пользователя.
        /// </summary>
        [JsonProperty("answer_id")]
        public long AnswerID { get; set; }

        /// <summary>
        /// Массив, содержащий объекты с вариантами ответа на вопрос в опросе.
        /// </summary>
        [JsonProperty("answers")]
        public List<VKPollAnswers> Answers { get; set; }

        /// <summary>
        /// Является ли опрос анонимным.
        /// </summary>
        [JsonProperty("anonymous")]
        public VKBoolean Anonymous { get; set; }
    }
}
