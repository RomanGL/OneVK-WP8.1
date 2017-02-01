namespace OneVK.Enums.Common
{
    /// <summary>
    /// Список прав ВКонтакте для приложения.
    /// </summary>
    public enum VKScopeList
    {
        /// <summary>
        /// Отправление уведомлений.
        /// </summary>
        notify,
        /// <summary>
        /// Доступ к друзьям.
        /// </summary>
        friends,
        /// <summary>
        /// Доступ к фотографиям.
        /// </summary>
        photos,
        /// <summary>
        /// Доступ к аудиозаписям.
        /// </summary>
        audio,
        /// <summary>
        /// Доступ к видеозаписям.
        /// </summary>
        video,
        /// <summary>
        /// Доступ к документам.
        /// </summary>
        docs,
        /// <summary>
        /// Доступ к заметкам.
        /// </summary>
        notes,
        /// <summary>
        /// Доступ к wiki-страницам.
        /// </summary>
        pages,
        /// <summary>
        /// Доступ к статусу пользователя.
        /// </summary>
        status,
        /// <summary>
        /// Доступ к группам пользователя.
        /// </summary>
        groups,
        /// <summary>
        /// Доступ к расширенным методам работы со стеной.
        /// </summary>
        wall,
        /// <summary>
        /// Доступ к расширенным методам работы с сообщениями.
        /// </summary>
        messages,
        /// <summary>
        /// Доступ к оповещениям об ответах пользователю.
        /// </summary>
        notifications,
        /// <summary>
        /// Доступ к статистике групп и приложений пользователя, администратором которых он является.
        /// </summary>
        stats,
        /// <summary>
        /// Доступ к API-ВКонтакте без переавторизации.
        /// </summary>
        offline
    }
}
