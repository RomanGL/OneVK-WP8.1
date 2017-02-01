namespace OneVK.Enums.Photo
{
    /// <summary>
    /// Сортировка списка фотографий.
    /// </summary>
    public enum VKPhotoSortDateLikes : byte
    {
        /// <summary>
        /// Сортировать по дате загрузки.
        /// </summary>
        ByDate = 0,
        /// <summary>
        /// Сортировать по количеству лайков.
        /// </summary>
        ByLikesCount = 1
    }
}
