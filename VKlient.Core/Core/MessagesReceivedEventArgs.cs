using OneVK.Model.LongPoll;
using System;
using System.Collections.Generic;

namespace OneVK.Core
{
    /// <summary>
    /// Содержит информацию о событии получения новых сообщений через LongPoll.
    /// </summary>
    public sealed class MessagesReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Словарь полученных сообщений (диалог-сообщения).
        /// </summary>
        public List<MessageInfo> Messages { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса с заданным списком полученных сообщений.
        /// </summary>
        /// <param name="messages">Словарь полученных сообщений.</param>
        internal MessagesReceivedEventArgs(List<MessageInfo> messages)
        {
            Messages = messages;
        }
    }
}
