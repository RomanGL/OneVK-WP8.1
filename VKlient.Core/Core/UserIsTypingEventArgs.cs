using System;
using OneVK.Model.LongPoll;

namespace OneVK.Core
{
    /// <summary>
    /// Информация о событии набора пользователем сообщения в чате или диалоге.
    /// </summary>
    public sealed class UserIsTypingEventArgs : EventArgs
    {
        /// <summary>
        /// Данные.
        /// </summary>
        public UserIsTypingInfo Info { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданной информации о событии набора сообщения.
        /// </summary>
        /// <param name="info">Информация о событии набора сообщения.</param>
        internal UserIsTypingEventArgs(UserIsTypingInfo info)
        {
            Info = info;
        }
    }
}
