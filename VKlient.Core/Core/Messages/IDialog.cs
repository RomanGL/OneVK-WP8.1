using OneVK.Model.Profile;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Представляет диалог.
    /// </summary>
    public interface IDialog : IConversation
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        ulong UserID { get; set; }
        /// <summary>
        /// Краткий профиль пользователя.
        /// </summary>
        VKProfileShort User { get; set; }
    }
}
