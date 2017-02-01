namespace OneVK.Model.Common
{
    /// <summary>
    /// Представляет счетчик. Используется в интерфейсе.
    /// </summary>
    public sealed class OneCounter
    {
        /// <summary>
        /// Тип счетчика.
        /// </summary>
        public CounterType Type { get; set; }
        /// <summary>
        /// Количество.
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Тип счетчика.
        /// </summary>
        public enum CounterType : byte
        {
            /// <summary>
            /// Аудиозаписи.
            /// </summary>
            Audios,
            /// <summary>
            /// Видеозаписи.
            /// </summary>
            Videos,
            /// <summary>
            /// Страницы.
            /// </summary>
            Pages,
            /// <summary>
            /// Заметки.
            /// </summary>
            Notes,
            /// <summary>
            /// Фотографии.
            /// </summary>
            Photos,
            /// <summary>
            /// Альбомы.
            /// </summary>
            Albums,
            /// <summary>
            /// Друзья.
            /// </summary>
            Friends,
            /// <summary>
            /// Общие друзья.
            /// </summary>
            MutualFriends,
            /// <summary>
            /// Подписчики.
            /// </summary>
            Followers,
            /// <summary>
            /// Подписки.
            /// </summary>
            Subscriptions
        }
    }
}
