namespace OneVK.Enums.App
{
    /// <summary>
    /// Перечисление состояний контента.
    /// </summary>
    public enum ContentState : byte
    {
        /// <summary>
        /// Состояние отсутствует.
        /// </summary>
        None = 0,
        /// <summary>
        /// Контент загружен и может быть показан.
        /// </summary>
        Normal,
        /// <summary>
        /// Выполняется загрузка контента.
        /// </summary>
        Loading,
        /// <summary>
        /// Произошла ошибка.
        /// </summary>
        Error,
        /// <summary>
        /// Данные отсутствуют.
        /// </summary>
        NoData
    }
}
