using Newtonsoft.Json;
using OneVK.Core.Json;
using OneVK.Model.Message;
using OneVK.Model.Profile;
using System.Collections.Generic;
using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using OneVK.Core.Collections;

namespace OneVK.Core
{
    /// <summary>
    /// Представляет собой кэшированную беседу.
    /// </summary>    
    public class CachedConversationOld : ObservableObject, IEqualityComparer<CachedConversationOld>
    {
        private long _id;
        private string _title;
        private List<VKMessage> _cachedMessages;
        private ObservableCollection<VKProfileChat> _users;
        private MessagesCollection _messages;

        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        public long ID
        {
            get { return _id; }
            set { Set(nameof(ID), ref _id, value); }
        }

        /// <summary>
        /// Заголовок беседы.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { Set(nameof(Title), ref _title, value); }
        }

        /// <summary>
        /// Список кэшированных сообщений беседы.
        /// </summary>
        public List<VKMessage> CachedMessages
        {
            get { return _cachedMessages; }
            set { Set(nameof(CachedMessages), ref _cachedMessages, value); }
        }

        /// <summary>
        /// Список пользователей, участвующих в беседе.
        /// </summary>
        public ObservableCollection<VKProfileChat> Users
        {
            get { return _users; }
            set { Set(nameof(Users), ref _users, value); }
        }

        /// <summary>
        /// Коллекция сообщений беседы.
        /// </summary>
        [JsonIgnore]
        public MessagesCollection Messages
        {
            get { return _messages; }
            set { Set(() => Messages, ref _messages, value); }
        }

        /// <summary>
        /// Количество непрочитанных.
        /// </summary>
        public uint Unread { get; set; }

        /// <summary>
        /// Идентификатор создателя чата, если беседа является чатом.
        /// </summary>
        public ulong AdminID { get; set; }

        /// <summary>
        /// Сравнивает объекты на равенство.
        /// </summary>
        public bool Equals(CachedConversationOld x, CachedConversationOld y)
        {
            return x.ID == y.ID;
        }

        /// <summary>
        /// Возвращает хэш-код объекта.
        /// </summary>
        public int GetHashCode(CachedConversationOld obj)
        {
            return obj.Title.GetHashCode() + obj.ID.GetHashCode();
        }
    }
}
