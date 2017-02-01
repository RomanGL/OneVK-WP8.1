using Newtonsoft.Json;
using System;

namespace OneVK.Model.Profile
{
    /// <summary>
    /// Профиль пользователя в чате.
    /// </summary>
    public class VKProfileChat : VKProfileShort, IEquatable<VKProfileChat>
    {
        /// <summary>
        /// Идентификатор пользователя, который пригласил в беседу.
        /// </summary>
        [JsonProperty("invited_by")]
        public ulong InvitedByID { get; set; }

        /// <summary>
        /// Сравнивает экземпляр с текущим.
        /// </summary>
        /// <param name="other">Экземпляр для сравнения.</param>
        public bool Equals(VKProfileChat other)
        {
            return other.ID == this.ID;
        }
    }
}
