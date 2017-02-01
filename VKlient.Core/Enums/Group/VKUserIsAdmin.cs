namespace OneVK.Enums.Group
{
    /// <summary>
    /// Перечисление полномочий пользователя как руководителя группы.
    /// </summary>
    public enum VKUserIsAdmin : byte
    {
        /// <summary>
        /// Полномочия отсутствуют.
        /// </summary>
        None = 0,
        /// <summary>
        /// Полномочия модератора.
        /// </summary>
        Moderator = 1,
        /// <summary>
        /// Полномочия редактора.
        /// </summary>
        Editor = 2,
        /// <summary>
        /// Полномочия администратора.
        /// </summary>
        Admin = 3
    }
}
