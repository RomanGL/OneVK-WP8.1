namespace OneVK.Enums.Common
{
    /// <summary>
    /// Перечисление типов целевых объектов для отметки "Мне нравится".
    /// </summary>
    public enum VKLikesTargetType : byte
    {
        /// <summary>
        /// Запись на стене пользователя или сообщества.
        /// </summary>
        post = 0,
        /// <summary>
        /// Комментарий к записи на стене пользователя или сообщества.
        /// </summary>
        comment,
        /// <summary>
        /// Фотография.
        /// </summary>
        photo,
        /// <summary>
        /// Аудиозапись.
        /// </summary>
        audio,
        /// <summary>
        /// Видеозапись.
        /// </summary>
        video,
        /// <summary>
        /// Заметка.
        /// </summary>
        note,
        /// <summary>
        /// Комментарий к фотографии.
        /// </summary>
        photo_comment,
        /// <summary>
        /// Комментарий к видеозаписи.
        /// </summary>
        video_comment,
        /// <summary>
        /// Комментарий в обсуждении.
        /// </summary>
        topic_comment
    }
}
