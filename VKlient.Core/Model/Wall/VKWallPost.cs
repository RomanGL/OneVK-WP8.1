using Newtonsoft.Json;

namespace OneVK.Model.Wall
{
    /// <summary>
    /// Базовый объект записи со стены пользователя или сообщества.
    /// </summary>
    public class VKWallPost : BaseVKPost
    {
        /// <summary>
        /// Идентификатор записи на стене ее владельца.
        /// </summary>
        [JsonProperty("id")]
        public override ulong ID { get; set; }

        /// <summary>
        /// Идентификатор владельца стены, на которой размещена запись.
        /// </summary>
        [JsonProperty("owner_id")]
        public override long OwnerID { get; set; }

        /// <summary>
        /// Идентификатор автора записи.
        /// </summary>
        [JsonProperty("from_id")]
        public long FromID { get; set; }
    }
}