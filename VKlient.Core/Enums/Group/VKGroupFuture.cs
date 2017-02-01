namespace OneVK.Enums.Group
{
    /// <summary>
    /// В зависимости от параметра присылается список с будующими событиями
    /// </summary>
    public enum VKGroupFuture : byte
    {
        /// <summary>
        /// Будущие (предстоящие) события
        /// </summary>
        Future = 1,
        /// <summary>
        /// Все события
        /// </summary>
        False = 0
    }
}
