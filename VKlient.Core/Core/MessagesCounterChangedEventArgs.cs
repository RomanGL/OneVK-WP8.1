using System;

namespace OneVK.Core
{
    /// <summary>
    /// Содержит информацию о событии изменения счетчика непрочитанных сообщений.
    /// </summary>
    public sealed class MessagesCounterChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Количество непрочитанных сообщений.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным количеством непрочитанных сообщений.
        /// </summary>
        /// <param name="count">Количество непрочитанных сообщений.</param>
        internal MessagesCounterChangedEventArgs(int count)
        {
            Count = count;
        }
    }
}
