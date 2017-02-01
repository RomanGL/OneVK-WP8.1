namespace OneVK.Enums.Common
{
    /// <summary>
    /// Тип жалобы.
    /// </summary>
    public enum VKReason : byte
    {
        /// <summary>
        /// Спам.
        /// </summary>
        Spam = 0,
        /// <summary>
        /// Детская порнография.
        /// </summary>
        ChildPornography = 1,
        /// <summary>
        /// Экстремизм.
        /// </summary>
        Extremism = 2,
        /// <summary>
        /// Насилие.
        /// </summary>
        Violence = 3,
        /// <summary>
        /// Пропаганда наркотиков.
        /// </summary>
        DrugDropaganda = 4,
        /// <summary>
        /// Материал для взрослых.
        /// </summary>
        AdultMaterial = 5,
        /// <summary>
        /// Оскорбление.
        /// </summary>
        Insult = 6
    }
}
