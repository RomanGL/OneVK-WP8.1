using OneVK.Model.Message;
using OneVK.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Request;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Service;
using OneVK.Helpers;
using Windows.UI.Core;
using System.Collections.Specialized;
using OneVK.Enums.Message;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using System.ComponentModel;
using OneVK.Service.Messages;
using Microsoft.Practices.ServiceLocation;
using OneVK.Service.Common;
using OneVK.Core.Messages;

namespace OneVK.Core.Collections
{
    public class MessagesCollection : StateSupportCollection<VKMessage>, ISupportUpDownIncrementalLoading
    {
        private static VKMessageEqualityComparer comparer = new VKMessageEqualityComparer();
        private uint totalCount = uint.MaxValue;
        private int offset = -13;
        private bool isInitialized;
        private bool isInitializing;
        private IMessagesUsersService _usersService;
        private readonly object lockObject = new object();

        #region Конструкторы

        /// <summary>
        /// Инициализирует новый экземпляр коллекции сообщений для указанного чата.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        public MessagesCollection(uint chatID)
        {
            this.ChatID = chatID;
            this.TypingUsers = new ObservableCollection<KeyValuePair<DispatcherTimer, VKProfileShort>>();
            HasMoreDownItems = true;
            HasMoreUpItems = true;
            ShowUsersCount = true;
            NewSentMessages = new List<VKMessage>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции сообщений для указанного пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        public MessagesCollection(ulong userID)
        {
            this.UserID = userID;
            this.TypingUsers = new ObservableCollection<KeyValuePair<DispatcherTimer, VKProfileShort>>();
            HasMoreDownItems = true;
            HasMoreUpItems = true;
            NewSentMessages = new List<VKMessage>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции сообщений для указанного чата.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        /// <param name="messages">Коллекция сообщений для инициализации.</param>
        public MessagesCollection(uint chatID, IEnumerable<VKMessage> messages)
            : base(messages)
        {
            this.ChatID = chatID;
            this.TypingUsers = new ObservableCollection<KeyValuePair<DispatcherTimer, VKProfileShort>>();
            HasMoreDownItems = true;
            HasMoreUpItems = true;
            ShowUsersCount = true;
            NewSentMessages = new List<VKMessage>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр коллекции сообщений для указанного пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="messages">Коллекция сообщений для инициализации.</param>
        public MessagesCollection(ulong userID, IEnumerable<VKMessage> messages)
            : base(messages)
        {
            this.UserID = userID;
            this.TypingUsers = new ObservableCollection<KeyValuePair<DispatcherTimer, VKProfileShort>>();
            HasMoreDownItems = true;
            HasMoreUpItems = true;
            NewSentMessages = new List<VKMessage>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр пустой коллекции с заданным состоянием контента.
        /// </summary>
        /// <param name="state">Состояние контента.</param>
        public MessagesCollection(ContentState state)
            :base()
        {
            this.State = state;
        }

        #endregion

        /// <summary>
        /// Возвращает сервис для работы с сообщениями.
        /// </summary>
        private IMessagesUsersService UsersService
        {
            get
            {
                if (_usersService == null) _usersService = ServiceLocator.Current.GetInstance<IMessagesUsersService>();
                return _usersService;
            }
        }

        /// <summary>
        /// Идентификатор чата.
        /// </summary>
        public uint ChatID { get; private set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public ulong UserID { get; private set; }

        /// <summary>
        /// Является ли беседа чатом.
        /// </summary>
        public bool IsChat { get { return ChatID != 0; } }

        /// <summary>
        /// Следует ли показать количество участников.
        /// </summary>
        public bool ShowUsersCount { get; private set; }

        /// <summary>
        /// Колличество пропущенных сообщений.
        /// </summary>
        public uint Skipped { get; private set; }

        /// <summary>
        /// Количество непрочитанных сообщений.
        /// </summary>
        public uint Unread { get; set; }
        
        /// <summary>
        /// Имеются ли еще элементы внизу списка.
        /// </summary>
        public bool HasMoreDownItems { get; private set; }

        /// <summary>
        /// Имеются ли еще элементы вверху списка.
        /// </summary>
        public bool HasMoreUpItems { get; private set; }

        /// <summary>
        /// Имеются ли новые отправленные сообщения.
        /// </summary>
        public List<VKMessage> NewSentMessages { get; private set; }

        /// <summary>
        /// Возвращает коллекцию печатающих пользователей.
        /// </summary>
        public ObservableCollection<KeyValuePair<DispatcherTimer, VKProfileShort>> TypingUsers { get; private set; }

        public override void Load()
        {
            HasMoreUpItems = true;
            HasMoreDownItems = true;
        }

        /// <summary>
        /// Выполнить инициализацию.
        /// </summary>
        public async Task Initialize()
        {
            isInitializing = true;
            isInitialized = await LoadFromLastUnread();
            if (!isInitialized)
            {
                HasMoreUpItems = false;
                HasMoreDownItems = false;
            }
            isInitializing = false;
        }

        /// <summary>
        /// Сбрасывает коллекцию.
        /// </summary>
        protected override void Reset()
        {
            offset = -13;
            Skipped = 0;
            totalCount = uint.MaxValue;
        }

        /// <summary>
        /// Загрузить следующую партию элементов в конец списка.
        /// </summary>
        /// <param name="count">Количество элементов для загрузки.</param>
        public async Task<object> LoadDownAsync(uint count)
        {
            if (!isInitialized && !isInitializing)
            {
                await Initialize();
                return this.LastOrDefault(m => m.ReadState == VKBoolean.False);
            }

            var lastMsg = this.LastOrDefault();
            if (lastMsg == null) return null;

            State = ContentState.Loading;
            var msgs = await GetMessagesWithParameters(0, 20, (long)lastMsg.ID);
            if (msgs == null)
            {
                State = ContentState.Error;
            }
            else
            {
                for (int i = 1; i < msgs.Count; i++)
                {
                    this.Add(msgs[i]);
                }                
                HasMoreDownItems = totalCount > Count;
                State = ContentState.Normal;
            }

            await CacheCurrentConversation();
                        
            return null;
        }

        /// <summary>
        /// Загрузить следующую партию элементов в начало списка.
        /// </summary>
        /// <param name="count">Количество элементов для загрузки.</param>
        public async Task<object> LoadUpAsync(uint count)
        {
            if (!isInitialized && !isInitializing)
            {
                await Initialize();
                return this.LastOrDefault(m => m.ReadState == VKBoolean.False);
            }

            await LoadFromLastUnread();
            return this.FirstOrDefault();
        }

        public async Task<bool> LoadFromLastUnread()
        {
            if (State == ContentState.Loading) return false;
            State = ContentState.Loading;
            
            if (Skipped > 0) offset -= 20;

            var msgs = await GetMessagesWithParameters(offset, 20, -1);
            if (msgs == null)
            {
                State = ContentState.Error;
                return false;
            }
            else if (msgs.Count == 0)
            {
                State = ContentState.NoData;
                HasMoreUpItems = false;
                return false;
            }

            msgs.Reverse();
            await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                int insertIndex = 0;
                foreach (var msg in msgs)
                {
                    var containsMsg = this.FirstOrDefault(m => m.ID == msg.ID);
                    if (containsMsg != null)
                    {
                        insertIndex = this.IndexOf(containsMsg);
                        continue;
                    }

                    containsMsg = this.LastOrDefault(m => m.ID > msg.ID);
                    if (containsMsg != null) insertIndex = this.IndexOf(containsMsg) + 1;

                    this.Insert(insertIndex, msg);
                }

                var fReaded = this.FirstOrDefault(m => m.ReadState == VKBoolean.True);
                if (this.IndexOf(fReaded) != this.Count - 1)
                {
                    foreach (var msg in this)
                        msg.ReadState = VKBoolean.True;
                }
            });
                        
            State = ContentState.Normal;
            HasMoreUpItems = Skipped > 0;
            await CacheCurrentConversation();
            return true;     
        }

        public async Task LoadFromMessageID(long messageID)
        {
            if (State == ContentState.Loading) return;
            State = ContentState.Loading;
            
            var msgs = await GetMessagesWithParameters(-10, 20, messageID);
            if (msgs == null)
            {
                State = ContentState.Error;
                return;
            }
        }

        /// <summary>
        /// Устанавливает режим набора пользователем сообщения.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        public async void SetUserIsTyping(ulong userID)
        {
            lock (lockObject)
            {
                var pair = TypingUsers.FirstOrDefault(p => p.Value.ID == userID);
                if (pair.Key != null)
                {
                    pair.Key.Stop();
                    pair.Key.Start();
                    return;
                }
            }

            var users = await UsersService.GetConversationUsers(ChatID != 0 ? -ChatID : (long)UserID);
            var newPair = new KeyValuePair<DispatcherTimer, VKProfileShort>(
                new DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) },
                users.FirstOrDefault(u => u.ID == userID));

            newPair.Key.Tick += TypingTimer_Tick;
            newPair.Key.Start();

            lock (lockObject)
            {
                TypingUsers.Add(newPair);
                ShowUsersCount = TypingUsers.Count == 0 && IsChat;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TypingUsers)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ShowUsersCount)));
            }
        }

        /// <summary>
        /// Вызывается при срабатывании таймера.
        /// </summary>
        private void TypingTimer_Tick(object sender, object e)
        {
            lock (TypingUsers)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                timer.Tick -= TypingTimer_Tick;
                var pair = TypingUsers.FirstOrDefault(p => p.Key == timer);
                TypingUsers.Remove(pair);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TypingUsers)));
            }            
        }

        /// <summary>
        /// Возвращает коллекцию сообщений по указанным параметрам.
        /// </summary>
        /// <param name="offset">Смещение для выборки определенного множества.</param>
        /// <param name="count">Количество элементов для выборки.</param>
        /// <param name="startMessageID"></param>
        private async Task<List<VKMessage>> GetMessagesWithParameters(int offset, uint count, long startMessageID = 0, bool rev = false)
        {
            var request = new GetHistoryRequest
            {
                Offset = offset,
                Count = count,
                StartMessageID = startMessageID,
                Reverse = rev ? VKBoolean.True : VKBoolean.False
            };
            if (IsChat) request.ChatID = ChatID;
            else request.UserID = UserID;

            List<VKMessage> result = null;

            var response = await request.ExecuteAsync();
            if (response.Error.ErrorType == VKErrors.None)
            {
                Skipped = response.Response.Skipped;
                Unread = response.Response.Unread;
                totalCount = response.Response.Count;

                if (response.Response.Items.Count == 0)
                    return new List<VKMessage>();                  

                try { await UsersService.SetUsers(response.Response.Items); }
                catch (Exception) { return null; }
                
                result = response.Response.Items;
            }

            return result;
        }

        /// <summary>
        /// Кэширует текущую беседу.
        /// </summary>
        private async Task CacheCurrentConversation()
        {
            try
            {
                var service = ServiceLocator.Current.GetInstance<IMessagesCacheService>();
                IConversation conv = await service.GetConversation(ChatID != 0 ? -ChatID : (long)UserID);
                await service.CacheConversation(conv);
            }
            catch (Exception ex) { ServiceLocator.Current.GetInstance<ILogService>().Log(ex); }

            return;
        }
    }
}
