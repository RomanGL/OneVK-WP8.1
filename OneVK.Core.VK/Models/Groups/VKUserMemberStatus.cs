namespace OneVK.Core.VK.Models.Groups
{
    /// <summary>
    /// Статус участника текущего пользователя в сообществе.
    /// </summary>
    public enum VKUserMemberStatus : byte
    {
        /// <summary>
        /// Не является участником.
        /// </summary>
        NotMember = 0,
        /// <summary>
        /// Участник сообщества.
        /// </summary>
        IsMember = 1,
        /// <summary>
        /// Не уверен.
        /// </summary>
        NotSure = 2,
        /// <summary>
        /// Отклонил приглашение.
        /// </summary>
        InviteRejected = 3,
        /// <summary>
        /// Подал заявку на вступление.
        /// </summary>
        ApplicationSubmited = 4,
        /// <summary>
        /// Приглашен.
        /// </summary>
        Invited = 5
    }
}
