namespace OneVK.Enums.Common
{
    /// <summary>
    /// Список уровней доступа к альбому и к комментированию альбома (только для альбомов пользователя).
    /// </summary>
    public enum VKAlbumPrivacy : byte
    {
        /// <summary>
        /// Все пользователи.
        /// </summary>
        AllUsers = 0,
        /// <summary>
        /// Только друзья.
        /// </summary>
        FriendsOnly = 1,
        /// <summary>
        /// Друзья и друзья друзей.
        /// </summary>
        FriendsOfFriends = 2,
        /// <summary>
        /// Только я.
        /// </summary>
        OnlyMe = 3
    }
}
