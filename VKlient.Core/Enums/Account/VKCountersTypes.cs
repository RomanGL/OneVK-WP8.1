namespace OneVK.Enums.Account
{
    /// <summary>
    /// Тип счетчиков для пользователя.
    /// </summary>
    public enum VKCountersTypes : byte
    {
        /// <summary>
        /// Счетчик друзей.
        /// </summary>
        Friends = 0,
        /// <summary>
        /// Счетчик новых фотографий.
        /// </summary>
        Photos,
        /// <summary>
        /// Счетчик новых сообщений.
        /// </summary>
        Messages,
        /// <summary>
        /// Счетчик новых видеозаписей.
        /// </summary>
        Videos,
        /// <summary>
        /// Счетчик заметок.
        /// </summary>
        Notes,
        /// <summary>
        /// Счетчик новых подарков.
        /// </summary>
        Gifts,
        /// <summary>
        /// Счетчик событий.
        /// </summary>
        Events,
        /// <summary>
        /// Счетчик групп.
        /// </summary>
        Groups,
        /// <summary>
        /// Счетчик уведомлений.
        /// </summary>
        Notifications
    }
}
