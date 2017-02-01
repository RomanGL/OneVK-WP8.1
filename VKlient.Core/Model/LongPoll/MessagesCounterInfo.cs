
namespace OneVK.Model.LongPoll
{
    /// <summary>
    /// Содержит информацию о смене данных счетчика непрочитанных сообщений.
    /// </summary>
    public sealed class MessagesCounterInfo
    {
        /// <summary>
        /// Количество непрочитанных сообщений.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным количеством непрочитанных сообщений.
        /// </summary>
        /// <param name="count">Количество непрочитанных сообщений.</param>
        internal MessagesCounterInfo(int count)
        {
            Count = count;
        }
    }
}
