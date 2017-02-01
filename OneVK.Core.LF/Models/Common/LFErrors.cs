namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Перечисление ошибок, возвращаемых Last.fm.
    /// </summary>
    public enum LFErrors : byte
    {
        /// <summary>
        /// Ошибки не произошло.
        /// </summary>
        None = 0,
        /// <summary>
        /// Неизвестная ошибка.
        /// </summary>
        UnknownError = 1,
        /// <summary>
        /// Сервис не существует.
        /// </summary>
        InvalidService = 2,
        /// <summary>
        /// Неверный метод.
        /// </summary>
        InvalidMethod = 3,
        /// <summary>
        /// Ошибка авторизации. Нет прав для доступа.
        /// </summary>
        AuthFailed = 4,
        /// <summary>
        /// Неверный формат.
        /// </summary>
        InvalidFormat = 5,
        /// <summary>
        /// Неверные параметры.
        /// </summary>
        InvalidParameters = 6,
        /// <summary>
        /// Указан неверный ресурс.
        /// </summary>
        InvalidResourceSpecified = 7,
        /// <summary>
        /// Операция не завершена. Возможно сервер испытывает проблемы.
        /// Повторите попытку позже.
        /// </summary>
        OpeartionFailed = 8,
        /// <summary>
        /// Неверный ключ сессии.
        /// </summary>
        InvalidSessionKey = 9,
        /// <summary>
        /// Неверный API-ключ.
        /// </summary>
        InvalidAPIKey = 10,
        /// <summary>
        /// Сервис отключен. Повторите попытку позже.
        /// </summary>
        ServiceOffline = 11,
        /// <summary>
        /// Доступно только платным подписчикам.
        /// </summary>
        SubscribersOnly = 12,
        /// <summary>
        /// Неверная сигнатура метода.
        /// </summary>
        InvalidMethodSignature = 13,
        /// <summary>
        /// Неавторизированный токен.
        /// </summary>
        UnathorizedToken = 14,
        /// <summary>
        /// Потоковая передача недоступна.
        /// </summary>
        StreamingUnavaible = 15,
        /// <summary>
        /// Сервис временно недоступен.
        /// </summary>
        ServiceTemporarilyUnavaible = 16,
        /// <summary>
        /// Требуется авторизация.
        /// </summary>
        LoginNeeded = 17,
        /// <summary>
        /// Пробный период истек. Требуется платная подписка.
        /// </summary>
        TrialExperied = 18,
        /// <summary>
        /// Неизвестная ошибка.
        /// </summary>
        UnknownError2 = 19,
        /// <summary>
        /// Недостаточно контента для проигрывания данной станции.
        /// </summary>
        NotEnoughContent = 20,
        /// <summary>
        /// Недостаточно участников группы для радио.
        /// </summary>
        NotEnoughMembers = 21,
        /// <summary>
        /// Недостаточно фанатов исполнителя для радио.
        /// </summary>
        NotEnoughFans = 22,
        /// <summary>
        /// Недостаточно соседей для радио.
        /// </summary>
        NotEnoughNeighbours = 23,
        /// <summary>
        /// Не разрешается слушать радио при пиковой нагрузке.
        /// </summary>
        NoPeakRadio = 24,
        /// <summary>
        /// Радио не найдено.
        /// </summary>
        RadioNotFound = 25,
        /// <summary>
        /// API-ключ заморожен.
        /// </summary>
        ApiKeySuspended = 26,
        /// <summary>
        /// Данный тип запроса устарел и более не поддерживается.
        /// </summary>
        Deprecated = 27,
        /// <summary>
        /// Слишком много запросов.
        /// </summary>
        TooManyRequests = 29,
        /// <summary>
        /// Произошла ошибка соединения.
        /// </summary>
        ConnectionError = 126,
        /// <summary>
        /// Операция отменена.
        /// </summary>
        OperationCanceled = 127,
        /// <summary>
        /// Не удалось десериализовать объект. Данные повреждены.
        /// </summary>
        DeserializationError = 128
    }
}
