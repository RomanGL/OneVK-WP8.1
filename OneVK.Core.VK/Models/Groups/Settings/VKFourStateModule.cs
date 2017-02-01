namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Режим работы модуля с четырьмя состояниями.
    /// </summary>
    public enum VKFourStateModule : byte
    {
        /// <summary>
        /// Выключен.
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// Открыт.
        /// </summary>
        Public = 1,
        /// <summary>
        /// Ограничен (только группы и события).
        /// </summary>
        Limited = 2,
        /// <summary>
        /// Закрыт (только группы и события).
        /// </summary>
        Closed = 3
    }
}
