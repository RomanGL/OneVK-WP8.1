using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using OneVK.Core.Collections;
using OneVK.Core.Json;
using OneVK.Enums.App;
using OneVK.Model.Profile;
using System.Collections.ObjectModel;

namespace OneVK.Core.Messages
{
    /// <summary>
    /// Представляет многопользовательский чат.
    /// </summary>
    public sealed class Chat : ObservableObject, IChat
    {
        private string _title;
        private uint _unreadNumber;        
        private MessagesCollection _messages;
        private ObservableCollection<VKProfileChat> _users;

        /// <summary>
        /// Возвращает тип беседы.
        /// </summary>
        public ConversationType Type { get { return ConversationType.Chat; } }        
        /// <summary>
        /// Возвращает или задает идентификатор чата.
        /// </summary>
        public uint ChatID { get; set; }
        /// <summary>
        /// Возвращает или задает идентификатор создателя чата.
        /// </summary>
        public ulong AdminID { get; set; }
        /// <summary>
        /// Возвращает или задает идентификатор беседы.
        /// </summary>
        [JsonIgnore]
        public long ID
        {
            get { return -ChatID; }
            set { ChatID = (uint)(-value); }
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
        /// Возвращает или задает колличество непрочитанных сообщений в чате.
        /// </summary>
        public uint UnreadNumber
        {
            get { return _unreadNumber; }
            set { Set(nameof(UnreadNumber), ref _unreadNumber, value); }
        }

        /// <summary>
        /// Возвращает коллекцию сообщений чата.
        /// </summary>
        [JsonConverter(typeof(MessagesCollectionConverter))]
        public MessagesCollection Messages
        {
            get { return _messages; }
            set { Set(nameof(Messages), ref _messages, value); }
        }
        /// <summary>
        /// Возвращает или задает коллекцию пользователей чата.
        /// </summary>
        public ObservableCollection<VKProfileChat> Users
        {
            get { return _users; }
            set { Set(nameof(Users), ref _users, value); }
        }
    }
}
