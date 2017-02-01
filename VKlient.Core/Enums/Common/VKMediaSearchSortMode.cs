namespace OneVK.Enums.Common
{
    /// <summary>
    /// Способ сортировки результатов при поиске.
    /// </summary>
    public enum VKMediaSearchSortMode : byte
    {
        /// <summary>
        /// По дате.
        /// </summary>
        ByDate = 0,
        /// <summary>
        /// По длительности.
        /// </summary>
        ByDuration = 1,
        /// <summary>
        /// По популярности.
        /// </summary>
        ByPopularity = 2
    }
}
