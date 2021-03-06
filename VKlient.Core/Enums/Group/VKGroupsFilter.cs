﻿namespace OneVK.Enums.Groups
{
    /// <summary>
    /// Перечисление фильтров для получения списка сообществ.
    /// </summary>
    public enum VKGroupsFilter
    {
        /// <summary>
        /// Будут возвращены все сообщества.
        /// </summary>
        all,
        /// <summary>
        /// Будут возвращены сообщества, в которых пользователь является администратором.
        /// </summary>
        admin,
        /// <summary>
        /// Будут возвращены сообщества, в которых пользователь является администратором или редактором.
        /// </summary>
        editor,
        /// <summary>
        /// Будут возвращены сообщества, в которых пользователь является администратором, редактором или модератором.
        /// </summary>
        moder,
        /// <summary>
        /// Будут возвращены группы.
        /// </summary>
        groups,
        /// <summary>
        /// Будут возвращены публичные страницы.
        /// </summary>
        publics,
        /// <summary>
        /// Будут возвращены встречи.
        /// </summary>
        events
    }
}