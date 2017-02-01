using System;

namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Перечисление флагов сообщений.
    /// </summary>
    [Flags]
    public enum VKMessageFlags : ushort
    {
        /// <summary>
        /// Флаг не установлен.
        /// </summary>
        None = 0,
        /// <summary>
        /// Сообщение не прочитано.
        /// </summary>
        Unread = 1,
        /// <summary>
        /// Исходящее сообщение.
        /// </summary>
        Outbox = 2,
        /// <summary>
        /// На сообщение был создан ответ.
        /// </summary>
        Replied = 4,
        /// <summary>
        /// Сообщение помечено.
        /// </summary>
        Important = 8,
        /// <summary>
        /// Сообщение отправлено через чат.
        /// </summary>
        Chat = 16,
        /// <summary>
        /// Сообщение отправлено другом.
        /// </summary>
        Friends = 32,
        /// <summary>
        /// Сообщение помечено как спам.
        /// </summary>
        Spam = 64,
        /// <summary>
        /// Сообщение удалено (в корзине).
        /// </summary>
        Deleted = 128,
        /// <summary>
        /// Сообщение проверно пользователем на спам.
        /// </summary>
        Fixed = 256,
        /// <summary>
        /// В сообщении содержится медиаконтент.
        /// </summary>
        Media = 512
    }
}
