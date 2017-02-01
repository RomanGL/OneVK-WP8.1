namespace OneVK.Enums.Wall
{
    /// <summary>
    /// Фильтр для получения записей конкретного типа со стены
    /// пользователя или сообщества.
    /// </summary>
    public enum VKWallPostFilter
    {
        /// <summary>
        /// Все записи на стене.
        /// </summary>
        All,
        /// <summary>
        /// Предложенные записи на стене сообщества.
        /// </summary>
        Suggests,
        /// <summary>
        /// Отложенные записи.
        /// </summary>
        Postponed,
        /// <summary>
        /// Записи от владельца стены.
        /// </summary>
        Owner,
        /// <summary>
        /// Записи не от владельца стены.
        /// </summary>
        Others        
    }
}
