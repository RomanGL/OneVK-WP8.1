using Newtonsoft.Json;
using OneVK.Core.Json;

namespace OneVK.Model.Message
{
    /// <summary>
    /// Тип служебного сообщения чата.
    /// </summary>
    [JsonConverter(typeof(VKChatMessageActionTypeConverter))]
    public enum VKChatMessageActionType : byte
    {
        /// <summary>
        /// Это неслужебное сообщение.
        /// </summary>
        None = 0,
        /// <summary>
        /// Обновлена фотография беседы.
        /// </summary>
        ChatPhotoUpdate,
        /// <summary>
        /// Удалена фотография беседы.
        /// </summary>
        ChatPhotoRemove,
        /// <summary>
        /// Беседа создана.
        /// </summary>
        ChatCreate,
        /// <summary>
        /// Заголовок беседы обновлен.
        /// </summary>
        ChatTitleUpdate,
        /// <summary>
        /// В беседу добавлен новый пользователь.
        /// </summary>
        ChatInviteUser,
        /// <summary>
        /// Пользователь исключен из беседы.
        /// </summary>
        ChatKickUser
    }
}
