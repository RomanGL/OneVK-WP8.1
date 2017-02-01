namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Перечисление типов обновлений, полученных от LongPoll-сервера ВКонтакте.
    /// </summary>
    public enum VKLongPollUpdateType : byte
    {
        /// <summary>
        /// Сообщение удалено.
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
        MessageReceived = 4,
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
        /// Сброшены флаги фильтрации по папкам для чата или беседы.
        /// </summary>
        FolderFilterFlagsResetted = 10,
        /// <summary>
        /// Заменены флаги фильтрации по папкам для чата или беседы.
        /// </summary>
        FolderFilterFlagsReplaced = 11,
        /// <summary>
        /// Установлены флаги фильтрации по папкам для чата или беседы.
        /// </summary>
        FolderFilterFlagsSetted = 12,
        /// <summary>
        /// Флаги всех сообщений с заданным peer_id заменены.
        /// </summary>
        PeerMessagesFlagsReplaced = 13,
        /// <summary>
        /// Флаги всех сообщений с заданным peer_id установлены.
        /// </summary>
        PeerMessagesFlagsSetted = 14,
        /// <summary>
        /// Флаги всех сообщений с заданным peer_id сброшны.
        /// </summary>
        PeerMessagesFlagsResetted = 15,
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
        MessagesCounterChanged = 80,
        /// <summary>
        /// Изменены настройки оповещений чата или беседы.
        /// </summary>
        NotificationsSettingsChanged = 114
    }
}
