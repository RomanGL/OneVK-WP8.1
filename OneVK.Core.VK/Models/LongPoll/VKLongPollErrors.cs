namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Перечисление ошибок, возникающих приработе с LongPoll-сервером ВКонтакте.
    /// </summary>
    public enum VKLongPollErrors : byte
    {
        /// <summary>
        /// Ошибки не произошло.
        /// </summary>
        None = 0,
        /// <summary>
        /// История за определенный промежуток устарела и/или потеряна.
        /// Используйте новый параметр ts.
        /// </summary>
        DataIsOutdated = 1,
        /// <summary>
        /// Ключ подключения к серверу более недействителен.
        /// </summary>
        KeyIsExpired = 2,
        /// <summary>
        /// Информация была утеряна. Получите новый ключ и ts.
        /// </summary>
        DataIsCorrupted = 3
    }
}
