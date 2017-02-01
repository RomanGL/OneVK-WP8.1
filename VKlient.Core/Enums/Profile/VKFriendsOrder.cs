namespace OneVK.Enums.Profile
{
    /// <summary>
    /// Перечисление методов сортировки списка друзей.
    /// </summary>
    public enum VKFriendsOrder : byte
    {
        /// <summary>
        /// По имени (только при наличии параметре fields).
        /// </summary>
        Name = 0,
        /// <summary>
        /// По рейтингу. Как на сайте ВКонтакте.
        /// </summary>
        Hints,
        /// <summary>
        /// Случайный порядок.
        /// </summary>
        Random,
        /// <summary>
        /// Выше те, у кого установлено мобильное приложение.
        /// </summary>
        Mobile
    }
}
