namespace OneVK.Enums.Common
{
    /// <summary>
    /// Тип приватности.
    /// </summary>
    public enum VKPrivacyType
    {
        /// <summary>
        /// Ни один пользователь.
        /// </summary>
        nobody,
        /// <summary>
        /// Все пользователи.
        /// </summary>
        all,
        /// <summary>
        /// Друзья текущего пользователя.
        /// </summary>
        friends,
        /// <summary>
        /// Друзья друзей текущего пользователя.
        /// </summary>
        friends_of_friends,
        /// <summary>
        /// Определенный список пользователей, переданный в поле users. 
        /// </summary>
        users,
    }
}