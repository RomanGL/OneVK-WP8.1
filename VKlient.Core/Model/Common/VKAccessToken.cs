namespace OneVK.Model.Common
{
    /// <summary>
    /// Ключ доступа ВКонтакте.
    /// </summary>
    public sealed class VKAccessToken
    {
        /// <summary>
        /// Ключ доступа.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Срок жизни ключа в секундах.
        /// </summary>
        public int ExpiresIn { get; set; }
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; set; }
    }
}
