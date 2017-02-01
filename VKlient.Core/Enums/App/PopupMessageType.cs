namespace OneVK.Enums.App
{
    /// <summary>
    /// Тип всплывающего уведомления.
    /// </summary>
    public enum PopupMessageType : byte
    {
        /// <summary>
        /// Стандартное уведомление.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Ошибка.
        /// </summary>
        Error = 1,
        /// <summary>
        /// Предупреждение.
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Информация.
        /// </summary>
        Info = 3
    }
}
