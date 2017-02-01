namespace OneVK.Enums.Common
{
    public enum VKCommentAttachmentType : byte
    {
        /// <summary>
        /// Фотография из альбома.
        /// </summary>
        Photo = 0,
        /// <summary>
        /// Фотография, загруженная напрямую.
        /// </summary>
        Posted_photo,
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
        /// Граффити.
        /// </summary>
        Graffiti,
        /// <summary>
        /// Ссылка на Web-страницу.
        /// </summary>
        Link,
        /// <summary>
        /// Заметка.
        /// </summary>
        Note,
        /// <summary>
        /// Фотография, загруженная сторонним приложением.
        /// </summary>
        App,
        /// <summary>
        /// Опрос.
        /// </summary>
        Poll,
        /// <summary>
        /// Вики-страница.
        /// </summary>
        Page,
        /// <summary>
        /// Альбом с фотографиями.
        /// </summary>
        Album,
        /// <summary>
        /// Список фотографий, размещенных в одном посте.
        /// </summary>
        Photos_list,
        /// <summary>
        /// Стикер.
        /// </summary>
        Sticker
    }
}
