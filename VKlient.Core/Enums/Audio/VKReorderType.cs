namespace OneVK.Enums.Audio
{
    /// <summary>
    /// Тип переноса аудиозаписи в списке.
    /// </summary>
    public enum VKReorderType : byte
    {
        /// <summary>
        /// Переместить после указанной адиозаписью.
        /// </summary>
        After = 0,
        /// <summary>
        /// Переместить перед указанной аудиозаписью.
        /// </summary>
        Before = 1
    }
}
