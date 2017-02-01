namespace OneVK.Enums.App
{
    /// <summary>
    /// Тип навигации для сообщения с требованием перехода.
    /// </summary>
    public enum NavigationType : byte
    {
        /// <summary>
        /// Перейти на новую страницу.
        /// </summary>
        New = 0,
        /// <summary>
        /// Перейти на новую страницу с очисткой
        /// журнала переходов.
        /// </summary>
        NewClear,
        /// <summary>
        /// Вернуться назад.
        /// </summary>
        GoBack,
        /// <summary>
        /// Перейти вперед.
        /// </summary>
        GoForward
    }
}
