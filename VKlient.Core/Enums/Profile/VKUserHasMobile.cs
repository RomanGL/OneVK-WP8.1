namespace OneVK.Enums.Profile
{
    /// <summary>
    /// Перечисление, известен ли номер мобильного телефона пользователя.
    /// </summary>
    public enum VKUserHasMobile : byte
    {
        /// <summary>
        /// Номер не известен.
        /// </summary>
        False = 0,
        /// <summary>
        /// Номер известен.
        /// </summary>
        True
    }
}
