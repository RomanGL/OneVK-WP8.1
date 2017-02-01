namespace OneVK.Enums.Profile
{
    /// <summary>
    /// Перечисление падежей имен пользователей.
    /// </summary>
    public enum VKUserNameCase : byte
    {
        /// <summary>
        /// Именительный падеж.
        /// </summary>
        nom = 0,
        /// <summary>
        /// Родительный падеж.
        /// </summary>
        gen,
        /// <summary>
        /// Дательный падеж.
        /// </summary>
        dat,
        /// <summary>
        /// Винительный падеж.
        /// </summary>
        acc,
        /// <summary>
        /// Творительный падеж.
        /// </summary>
        ins,
        /// <summary>
        /// Предложный падеж.
        /// </summary>
        abl
    }
}
