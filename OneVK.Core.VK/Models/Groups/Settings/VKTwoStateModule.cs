namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Режим работы модуля с двумя состояниями.
    /// </summary>
    public enum VKTwoStateModule : byte
    {
        /// <summary>
        /// Выключен.
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// Включен.
        /// </summary>
        Enabled = 1
    }
}
