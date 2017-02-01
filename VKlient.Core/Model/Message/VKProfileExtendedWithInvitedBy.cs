using Newtonsoft.Json;
using System;
using OneVK.Model.Profile;

namespace OneVK.Model.Message
{
    /// <summary>
    /// Расширенная нформация о пользователе с дополнительным полем InvitedBy.
    /// </summary>
    public class VKProfileExtendedWithInvitedBy : VKProfileExtended
    {
        /// <summary>
        /// Идентификатор пользователя, пригласившего в беседуу.
        /// </summary>
        [JsonProperty("invited_by")]
        public long InvitedBy { get; set; }
    }
}
