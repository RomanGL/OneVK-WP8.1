namespace OneVK.Enums.Newsfeed
{
    /// <summary>
    /// Тип записи со стены.
    /// </summary>
    public enum VKNewsfeedPostType
    {
        /// <summary>
        /// Запись на стене.
        /// </summary>
        post,
        /// <summary>
        /// Копия записи (репост) с другой стены.
        /// </summary>
        copy,
        /// <summary>
        /// Ответ на запись.
        /// </summary>
        reply,
        /// <summary>
        /// Отложенная запись.
        /// </summary>
        postpone,
        /// <summary>
        /// Предложенная запись.
        /// </summary>
        suggest
    }
}
