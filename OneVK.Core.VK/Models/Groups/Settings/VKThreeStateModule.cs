namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Режим работы модуля с тремя состояниями.
    /// </summary>
    public enum VKThreeStateModule : byte
    {
        /// <summary>
        /// Выелючен.
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// Открытый.
        /// </summary>
        Public = 1,
        /// <summary>
        /// Ограниченный (только группы и события).
        /// </summary>
        Limited = 2
    }
}
