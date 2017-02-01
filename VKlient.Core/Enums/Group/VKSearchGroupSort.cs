namespace OneVK.Enums.Group
{
    /// <summary>
    /// Критерий сортировки групп в посике по указанному параметру
    /// </summary>
    public enum VKSearchGroupSort : byte
    {
        /// <summary>
        /// По количеству пользователей
        /// </summary>
        Users = 0,
        /// <summary>
        /// По скорости роста
        /// </summary>
        SpeedGrowing = 1,
        /// <summary>
        /// По дневной посещаемости
        /// </summary>
        UsersPerDay = 2,
        /// <summary>
        /// По отношению количества лайков к количествe пользователей
        /// </summary>
        UsersAndLikes = 3,
        /// <summary>
        /// По отношению количества комментариев к количеству пользователей
        /// </summary>
        CommentAndUsers = 4,
        /// <summary>
        /// По отношению количества записей в обсуждениях к количеству пользователей
        /// </summary>
        PostsAndUsers = 5
    }
}
