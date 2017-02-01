namespace OneVK.Model.Notifications
{
    /// <summary>
    /// Содержит информацию об ответе пользователя на опопвещение.
    /// </summary>
    public interface INotificationReply
    {
        /// <summary>
        /// Ответ пользователя на оповещение.
        /// </summary>
        VKNotificationReply Reply { get; set; }
    }
}
