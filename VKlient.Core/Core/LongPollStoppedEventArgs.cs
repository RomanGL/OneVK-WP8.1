using System;

namespace OneVK.Core
{
    /// <summary>
    /// Содержит данные о событии остановки LongPoll-сервиса ВКонтакте.
    /// </summary>
    public sealed class LongPollStoppedEventArgs : EventArgs
    {
        /// <summary>
        /// Причина остановки работы сервиса.
        /// </summary>
        public LongPollStopReason Reason { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданной причиной остановки сервиса.
        /// </summary>
        /// <param name="reason">Причина остановки LongPoll-сервиса.</param>
        internal LongPollStoppedEventArgs(LongPollStopReason reason)
        {
            Reason = reason;
        }
    }
}
