namespace OneVK.Core
{
    /// <summary>
    /// Перечисление причин остановки LongPoll-сервиса ВКонтакте.
    /// </summary>
    public enum LongPollStopReason : byte
    {
        /// <summary>
        /// Сервис остановлен пользователем.
        /// </summary>
        ByUser,
        /// <summary>
        /// Ошибка соединения с LongPoll-сервером. Не удалось подключиться к серверу за 5 попыток.
        /// </summary>
        ConnectionError,
        /// <summary>
        /// Не удалось получить данные для подключения к LongPoll-серверу
        /// за 5 попыток. 
        /// </summary>
        CantGetServerData
    }
}
