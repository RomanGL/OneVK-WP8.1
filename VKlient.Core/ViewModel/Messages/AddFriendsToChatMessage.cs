using OneVK.Model.Profile;
using System.Collections.Generic;

namespace OneVK.ViewModel.Messages
{
    /// <summary>
    /// Представляет сообщение на добавление пользователей в чат.
    /// </summary>
    public class AddFriendsToChatMessage
    {
        /// <summary>
        /// Указывает на необходимость отмены регистрации без выполнения действий.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        public uint ChatID { get; set; }

        /// <summary>
        /// Коллекция пользователей для добавления в чат.
        /// </summary>
        public List<VKProfileShort> Users { get; set; }
    }
}
