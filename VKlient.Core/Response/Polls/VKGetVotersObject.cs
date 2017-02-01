using Newtonsoft.Json;
using OneVK.Model.Profile;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет результат запроса на метод polls.getVoters.
    /// </summary>
    public class VKGetVotersObject
    {
        /// <summary>
        /// Идентификатор варианта ответа.
        /// </summary>
        [JsonProperty("answer_id")]
        public long AnswerID { get; set; }

        /// <summary>
        /// Список идентификаторов пользователей (с информацией о них).
        /// </summary>
        [JsonProperty("users")]
        public VKCountedItemsObject<VKProfileBase> Users { get; set; }
    }
}
