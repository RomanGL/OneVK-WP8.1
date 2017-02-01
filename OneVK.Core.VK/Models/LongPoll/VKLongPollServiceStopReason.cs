namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Причина остановки сервиса LongPoll.
    /// </summary>
    public enum VKLongPollServiceStopReason : byte
    {
        /// <summary>
        /// Остановлен пользователем.
        /// </summary>
        ByUser = 0,
        /// <summary>
        /// Остановлн из-за ошибки соединения.
        /// </summary>
        ConnectionError,
        /// <summary>
        /// Внутренняя ошибка сервиса.
        /// </summary>
        InternalError
    }
}
