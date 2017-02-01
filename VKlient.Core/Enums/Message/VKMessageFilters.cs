namespace OneVK.Enums.Message
{
    /// <summary>
    /// Фильтр возвращаемых сообщений.
    /// </summary>
    public enum VKMessageFilters : byte
    {
        /// <summary>
        /// Все входящие или все исходящие сообщения.
        /// </summary>
        All = 0,
        /// <summary>
        /// Важные сообщения.
        /// </summary>
        Important = 8
    }
}
