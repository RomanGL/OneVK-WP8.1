namespace OneVK.Enums.LongPoll
{
    /// <summary>
    /// Перечисление типов обновлений, полученных от LongPoll-сервера ВКонтакте.
    /// </summary>
    public enum VKLongPollUpdateType : byte
    {
        /// <summary>
        /// Сообщения удалено.
        /// </summary>
        MessageDeleted = 0,
        /// <summary>
        /// Флаги сообщения заменены.
        /// </summary>
        MessageFlagsReplaced = 1,
        /// <summary>
        /// Флаги сообщения установлены.
        /// </summary>
        MessageFlagsSetted = 2,
        /// <summary>
        /// Флаги сообщени сброшены.
        /// </summary>
        MessageFlagsResetted = 3,
        /// <summary>
        /// Добавлено новое сообщение.
        /// </summary>
        NewMessage = 4,
        /// <summary>
        /// Входящие сообщения указанного диапазона прочитаны.
        /// </summary>
        ReceivedMessagesReaded = 6,
        /// <summary>
        /// Исходящие сообщения указанного диапазона прочитаны.
        /// </summary>
        SentMessagesReaded = 7,
        /// <summary>
        /// Пользователь стал онлайн.
        /// </summary>
        UserOnline = 8,
        /// <summary>
        /// Пользователь стал оффлайн.
        /// </summary>
        UserOffline = 9,
        /// <summary>
        /// Параметры чата были изменены.
        /// </summary>
        ChatParametersChanged = 51,
        /// <summary>
        /// Пользователь начал набирать текст в указанном диалоге.
        /// </summary>
        UserIsTypingInDialog = 61,
        /// <summary>
        /// Пользователь начал набирать текст в указанном чате.
        /// </summary>
        UserIsTypingInChat = 62,
        /// <summary>
        /// Указанный пользователь совершил звонок.
        /// </summary>
        UserMakesACall = 70,
        /// <summary>
        /// Изменилось значение счетчика непрочитанных сообщений.
        /// </summary>
        MessageCounterChanged = 80
    }
}
