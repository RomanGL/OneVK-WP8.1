using OneVK.Enums.Common;

namespace OneVK.Response
{
    /// <summary>
    /// Представляет ответ на запрос удаления 
    /// пользователя из списка друзей.
    /// </summary>
    public class VKDeleteFriendObject
    {
        /// <summary>
        /// Друг успешно удален.
        /// </summary>
        public VKOperationIsSuccess Success { get; set; }
        /// <summary>
        /// Друг удален.
        /// </summary>
        public VKOperationIsSuccess FriendDeleted { get; set; }
        /// <summary>
        /// Исходящая заявка удалена.
        /// </summary>
        public VKOperationIsSuccess OutRequestDeleted { get; set; }
        /// <summary>
        /// Входящая заявка удалена.
        /// </summary>
        public VKOperationIsSuccess InRequestDeleted { get; set; }
        /// <summary>
        /// Рекомендация друга удалена.
        /// </summary>
        public VKOperationIsSuccess SuggestionDeleted { get; set; }
    }
}
