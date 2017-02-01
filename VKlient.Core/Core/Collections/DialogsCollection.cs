using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Message;
using OneVK.Request.Execute;
using OneVK.Request.Message;
using OneVK.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace OneVK.Core.Collections
{
    /// <summary>
    /// Представляет коллекцию диалогов.
    /// </summary>
    public class DialogsCollection : StateSupportCollection<VKDialog>, ISupportUpDownIncrementalLoading
    {
        private uint totalCount = uint.MaxValue;
        private uint totalUnreadCount = uint.MaxValue;
        private MessagesService _service;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DialogsCollection"/>.
        /// </summary>
        public DialogsCollection()
            : base()
        { }

        /// <summary>
        /// Возвращает сервис для работы с сообщениями.
        /// </summary>
        private MessagesService Service
        {
            get
            {
                if (_service == null) _service = ServiceHelper.MessagesService;
                return _service;
            }
        }

        /// <summary>
        /// Имеются ли элементы для загрузки вниз.
        /// </summary>
        public bool HasMoreDownItems { get; private set; }

        /// <summary>
        /// Имеются ли эленты для загрузки вверх.
        /// </summary>
        public bool HasMoreUpItems { get { return false; } }

        /// <summary>
        /// Подгружает элементы в начало списка.
        /// </summary>
        /// <param name="count">Количество элементов для загрузки.</param>
        public Task<object> LoadUpAsync(uint count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Подгружает элементы в начало списка.
        /// </summary>
        /// <param name="count">Количество элементов для загрузки.</param>
        public async Task<object> LoadDownAsync(uint count)
        {
            List<VKDialog> dialogs = await GetDialogsWithParameters((uint)Count, 30);

            if (dialogs == null)
            {
                State = ContentState.Error;
                HasMoreDownItems = false;
                return null;
            }

            await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var d in dialogs)
                    this.Add(d);
            });

            HasMoreDownItems = totalCount > Count;
            State = ContentState.Normal;

            return null;
        }

        /// <summary>
        /// Обновляет коллекцию диалогов.
        /// </summary>
        public async Task Update()
        {
            List<VKDialog> dialogs = null;

            State = ContentState.Loading;
            if (this.Count == 0)
            {
                dialogs = await GetDialogsWithParameters(0, 30);

                if (dialogs == null)
                {
                    State = ContentState.Error;
                    HasMoreDownItems = false;
                    return;
                }

                await CoreHelper.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var d in dialogs)
                        this.Add(d);
                });

                HasMoreDownItems = totalCount > Count;
                State = ContentState.Normal;                
            }
            else
            {
                dialogs = await GetDialogsWithParameters(0, 30, true);
            }
        }

        /// <summary>
        /// Возвращает коллекцию диалогов по указанным параметрам.
        /// </summary>
        /// <param name="offset">Смещение списка.</param>
        /// <param name="count">Количество элементов.</param>
        /// <param name="unread">Вернуть только непрочитанные.</param>
        private async Task<List<VKDialog>> GetDialogsWithParameters(uint offset, uint count, bool unread = false)
        {
            var dialogsRequest = new ExecuteGetDialogsRequest
            {
                Offset = offset,
                Count = count,
                PreviewLength = 60,
                Unread = unread ? VKBoolean.True : VKBoolean.False
            };

            var dialogsResponse = await dialogsRequest.ExecuteAsync();

            if (dialogsResponse.Error.ErrorType != VKErrors.None) return null;

            if (unread) totalUnreadCount = dialogsResponse.Response.Dialogs.Count;
            else totalCount = dialogsResponse.Response.Dialogs.Count;

            foreach (var user in dialogsResponse.Response.Users)
                Service.CachedUsers[user.ID] = user;

            List<VKDialog> result = new List<VKDialog>();
            var unknownChats = new List<VKDialog>();
            foreach (var dialog in dialogsResponse.Response.Dialogs.Items)
            {
                dialog.Message.Sender = dialogsResponse.Response.Users.FirstOrDefault(u => u.ID == dialog.Message.UserID);

                if (dialog.IsChat)
                {
                    unknownChats.Add(dialog);
                }
                else if (dialog.Message.Sender != null)
                {
                    dialog.Message.Title = dialog.Message.Sender.FullName;
                    dialog.Photos = new ObservableCollection<string> { dialog.Message.Sender.Photo50 };
                }

                result.Add(dialog);
            }

            if (result.Count == 0) return result;

            var usersRequest = new GetChatsUsersRequest { ChatIDs = unknownChats.Select(d => d.Message.ChatID).ToList() };
            var usersResponse = await usersRequest.ExecuteAsync();

            if (usersResponse.Error.ErrorType != VKErrors.None) return null;

            foreach (uint chatID in usersResponse.Response.Keys)
            {
                var users = usersResponse.Response[chatID];
                var dialog = unknownChats.FirstOrDefault(d => d.Message.ChatID == chatID);
                dialog.Photos = new ObservableCollection<string>();

                if (!String.IsNullOrEmpty(dialog.Message.Photo50))
                {
                    dialog.Photos.Add(dialog.Message.Photo50);
                    continue;
                }

                foreach (var user in users)
                {
                    dialog.Photos.Add(user.Photo50);
                    Service.CachedUsers[user.ID] = user;
                }
            }

            return result;
        }

        /// <summary>
        /// Загрузить данные в коллекцию.
        /// </summary>
        public override async void Load()
        {
            if (this.Count == 0)
                await Update();
            else await LoadDownAsync(30);
        }

        /// <summary>
        /// Сбрасывает параметры коллекции к параметрам по умолчанию.
        /// </summary>
        protected override void Reset()
        {
            totalCount = uint.MaxValue;
            totalUnreadCount = uint.MaxValue;
        }
    }
}
