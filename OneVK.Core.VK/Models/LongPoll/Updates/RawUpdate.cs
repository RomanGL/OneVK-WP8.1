namespace OneVK.Core.VK.Models.LongPoll
{
    /// <summary>
    /// Представляет необработанные данные о LongPoll обновлении.
    /// </summary>
    public sealed class RawUpdate : IVKLongPollUpdate
    {
        /// <summary>
        /// Тип обновления.
        /// </summary>
        public VKLongPollUpdateType Type { get; internal set; }

        /// <summary>
        /// Массив с данными об обновлении.
        /// </summary>
        public object[] UpdateInfo { get; internal set; }

        internal RawUpdate() { }
    }
}
