using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using OneVK.Core.Collections;
using OneVK.Core.Json;
using OneVK.Enums.App;
using OneVK.Model.Profile;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Представляет диалог с одним пользователем.
    /// </summary>
    public sealed class Dialog : ObservableObject, IDialog
    {
        private string _title;
        private uint _unreadNumber;
        private VKProfileShort _user;
        private MessagesCollection _messages;

        /// <summary>
        /// Возвращает тип беседы.
        /// </summary>
        public ConversationType Type { get { return ConversationType.Dialog; } }
        /// <summary>
        /// Возвращает или задает идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; set; }
        /// <summary>
        /// Возвращает или задает идентификатор беседы.
        /// </summary>
        [JsonIgnore]
        public long ID
        {
            get { return (long)UserID; }
            set { UserID = (ulong)value; }
        }

        /// <summary>
        /// Возвращает или задает заголовок беседы.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { Set(nameof(Title), ref _title, value); }
        }
        /// <summary>
        /// Возвращает или задает колличество непрочитанных сообщений в диалоге.
        /// </summary>
        public uint UnreadNumber
        {
            get { return _unreadNumber; }
            set { Set(nameof(UnreadNumber), ref _unreadNumber, value); }
        }

        /// <summary>
        /// Возвращает или задает профиль пользователя.
        /// </summary>
        public VKProfileShort User
        {
            get { return _user; }
            set { Set(nameof(User), ref _user, value); }
        }
        /// <summary>
        /// Возвращает коллекцию сообщений диалога.
        /// </summary>
        [JsonConverter(typeof(MessagesCollectionConverter))]
        public MessagesCollection Messages
        {
            get { return _messages; }
            set { Set(nameof(Messages), ref _messages, value); }
        }
    }
}
