namespace OneVK.Enums.Profile
{
    /// <summary>
    /// Отношение к алкоголю.
    /// </summary>
    public enum VKAlcohol : byte
    {
        /// <summary>
        /// Неизвестно.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Резко негативное.
        /// </summary>
        VeryNegative = 1,
        /// <summary>
        /// Негативное.
        /// </summary>
        Negative,
        /// <summary>
        /// Нейтральное.
        /// </summary>
        Neutral,
        /// <summary>
        /// Компромиссное.
        /// </summary>
        Compromisable,
        /// <summary>
        /// Положительное.
        /// </summary>
        Positive
    }
}
