namespace OneVK.Enums.Profile
{
    /// <summary>
    /// Перечисление семейного положения пользователя.
    /// </summary>
    public enum VKRelation : byte
    {
        /// <summary>
        /// Неизвестно.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Не женат/не замужем.
        /// </summary>
        Single = 1,
        /// <summary>
        /// Есть друг/подруга.
        /// </summary>
        InARelationship,
        /// <summary>
        /// Помолвлен/помолвлена.
        /// </summary>
        Engaged,
        /// <summary>
        /// Женат/замужем.
        /// </summary>
        Married,
        /// <summary>
        /// Все сложно.
        /// </summary>
        ItsComplicated,
        /// <summary>
        /// В активном поиске.
        /// </summary>
        ActivelySearching,
        /// <summary>
        /// Влюблен/влюблена.
        /// </summary>
        InLove
    }
}
