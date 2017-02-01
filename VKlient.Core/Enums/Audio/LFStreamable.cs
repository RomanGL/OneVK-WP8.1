namespace OneVK.Enums.Audio
{
    /// <summary>
    /// Доступна ли потоковая передача с Last.fm..
    /// </summary>
    public enum LFStreamable : byte
    {
        /// <summary>
        /// Потоковая передача невозможна.
        /// </summary>
        False = 0,
        /// <summary>
        /// Потоковая передача доступна.
        /// </summary>
        True = 1
    }
}
