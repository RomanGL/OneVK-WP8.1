namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет уведомление с информацией об оповещении.
    /// </summary>
    /// <typeparam name="T">Тип информации об оповещении.</typeparam>
    public interface INotificationFeedback<T>
    {
        /// <summary>
        /// Информация об оповещении.
        /// </summary>
        T Feedback { get; set; }
    }
}
