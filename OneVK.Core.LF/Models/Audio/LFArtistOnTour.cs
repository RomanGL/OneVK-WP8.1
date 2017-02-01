namespace OneVK.Core.LF.Models
{
    /// <summary>
    /// Находится ли исполнитель в турне.
    /// </summary>
    public enum LFArtistOnTour : byte
    {
        /// <summary>
        /// Не в турне.
        /// </summary>
        False = 0,
        /// <summary>
        /// Исполнитель в турне.
        /// </summary>
        True = 1
    }
}
