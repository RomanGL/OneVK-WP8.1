namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет объект, который содержит объект-инициатор оповещения.
    /// </summary>
    public interface IActionObjectContainer
    {
        /// <summary>
        /// Объект-инициатор оповещения.
        /// </summary>
        INotificationActionObject ActionObject { get; set; }

        /// <summary>
        /// Возвращает идентификатор инициатора оповещения.
        /// </summary>
        long FromID { get; }
    }
}
