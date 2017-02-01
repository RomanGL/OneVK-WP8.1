namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Представляет оповещение с родительским элементом.
    /// </summary>
    /// <typeparam name="T">Тип родительского элемента.</typeparam>
    public interface INotificationParent<T>
    {
        /// <summary>
        /// Родительский элемент оповещения.
        /// </summary>
        T Parent { get; set; }
    }
}
