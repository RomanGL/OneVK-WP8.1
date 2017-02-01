namespace OneVK.Enums.Profile
{
    /// <summary>
    /// Перечисление, находится ли пользователь в черном списке у запрашиваемого.
    /// </summary>
    public enum VKUserBlacklisted : byte
    {
        /// <summary>
        /// Пользователь не в черном списке.
        /// </summary>
        False = 0,
        /// <summary>
        /// Пользователь в черном списке.
        /// </summary>
        True
    }
}
