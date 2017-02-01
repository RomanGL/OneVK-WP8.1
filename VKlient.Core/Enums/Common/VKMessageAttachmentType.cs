namespace OneVK.Enums.Common
{
    /// <summary>
    /// Перечисление типов вложений в личных сообщениях.
    /// </summary>
    public enum VKMessageAttachmentType : byte
    {
        /// <summary>
        /// Фотография.
        /// </summary>
        Photo = 0,
        /// <summary>
        /// Видеозапись.
        /// </summary>
        Video,
        /// <summary>
        /// Аудиозапись.
        /// </summary>
        Audio,
        /// <summary>
        /// Документ.
        /// </summary>
        Doc,
        /// <summary>
        /// Запись со стены.
        /// </summary>
        Wall,
        /// <summary>
        /// Комментарий к записи на стене.
        /// </summary>
        Wall_reply,
        /// <summary>
        /// Стикер.
        /// </summary>
        Sticker,
        /// <summary>
        /// Подарок.
        /// </summary>
        Gift,
        /// <summary>
        /// Ссылка.
        /// </summary>
        Link
    }
}
