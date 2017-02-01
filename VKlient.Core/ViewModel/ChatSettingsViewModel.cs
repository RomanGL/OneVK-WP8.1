using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OneVK.Core;
using OneVK.Core.Messages;
using OneVK.Enums.App;
using OneVK.Enums.Common;
using OneVK.Helpers;
using OneVK.Model.Profile;
using OneVK.Request;
using OneVK.Service.Messages;
using OneVK.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы настроек чата.
    /// </summary>
    public class ChatSettingsViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ChatSettingsViewModel"/>.
        /// </summary>
        public ChatSettingsViewModel(uint chatID)
        {
            ChatID = chatID;

            ChangeChatName = new RelayCommand(async () =>
            {
                IsWork = true;

                var request = new EditChatRequest(ChatID, ChatTitle);
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType == VKErrors.None && response.Response == VKOperationIsSuccess.True)
                {
                    Conversation.Title = ChatTitle;
                    NavigationHelper.GoBack();
                }
                else
                    await ServiceHelper.DialogService.ShowMessage("Не удалось сохранить параметры чата. Повторите попытку позднее.",
                        "Ошибка при сохранении");

                IsWork = false;
            });

            AddChatUsers = new RelayCommand(() =>
            {
                Messenger.Default.Register<AddFriendsToChatMessage>(this, ChatID, AddUsersToChat);
                
                if (ChoiceFriendsViewModel.CurrentChatID != 0)
                {
                    Messenger.Default.Send(new AddFriendsToChatMessage { Cancel = true }, 
                        ChoiceFriendsViewModel.CurrentChatID);
                }

                ChoiceFriendsViewModel.CurrentChatID = ChatID;
                NavigationHelper.Navigate(AppViews.ChoiceFriendsView);
            });

            RemoveUsers = new RelayCommand(async () =>
            {
                IsWork = true;

                bool notAll = false;
                var request = new RemoveChatUserRequest(ChatID, 1);
                foreach (var user in UsersToRemove)
                {
                    request.UserID = user.ID;
                    var response = await request.ExecuteAsync();

                    if (response.Error.ErrorType != VKErrors.None) notAll = true;
                    else Conversation.Users.Remove(user);

                    await Task.Delay(200);
                }

                if (notAll)
                    await ServiceHelper.DialogService.ShowMessage("Один или несколько пользователей не были исключены.", 
                        "Ошибка при исключении");

                IsWork = false;
            }, () => UsersToRemove != null && UsersToRemove.Count > 0);
        }
        #endregion

        #region Приватные поля
        private IChat _conversation;
        private string _chatTitle;
        private bool _isWork;
        #endregion

        #region Свойства
        /// <summary>
        /// Возвращает или задает заголовок чата.
        /// </summary>
        public string ChatTitle
        {
            get { return _chatTitle; }
            set { Set(() => ChatTitle, ref _chatTitle, value); }
        }

        /// <summary>
        /// Коллекция пользователей для исключения.
        /// </summary>
        public List<VKProfileChat> UsersToRemove { get; set; }

        /// <summary>
        /// Возвращает беседу.
        /// </summary>
        public IChat Conversation
        {
            get { return _conversation; }
            private set { Set(() => Conversation, ref _conversation, value); }
        }

        /// <summary>
        /// Возвращает или задает значение, выполняется ли сейчас работа.
        /// </summary>
        public bool IsWork
        {
            get { return _isWork; }
            set { Set(() => IsWork, ref _isWork, value); }
        }

        /// <summary>
        /// Возвращает идентфикатор чата.
        /// </summary>
        public uint ChatID { get; private set; }
        #endregion

        #region Команды
        /// <summary>
        /// Команда изменения имени чата.
        /// </summary>
        public RelayCommand ChangeChatName { get; private set; }
        /// <summary>
        /// Команда добавления пользователей чата.
        /// </summary>
        public RelayCommand AddChatUsers { get; private set; }
        /// <summary>
        /// Исключает пользователей из чата.
        /// </summary>
        public RelayCommand RemoveUsers { get; private set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override async void Activate(NavigationMode mode = NavigationMode.New)
        {
            try { Conversation = await ServiceLocator.Current.GetInstance<IMessagesCacheService>().GetConversation(-ChatID) as IChat; }
            catch (Exception) { }

            if (Conversation == null)
                Conversation = await ServiceLocator.Current.GetInstance<IConversationsService>().GetChat(ChatID);
            
            ChatTitle = Conversation.Title;
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Добавляет новых пользователей в чат.
        /// </summary>
        /// <param name="msg">Сообщение с информацией о пользователях.</param>
        private async void AddUsersToChat(AddFriendsToChatMessage msg)
        {
            Messenger.Default.Unregister<AddFriendsToChatMessage>(this, ChatID);
            if (msg.Cancel) return;
            ChoiceFriendsViewModel.CurrentChatID = 0;

            IsWork = true;

            bool notAll = false;
            var request = new AddChatUserRequest(msg.ChatID, 1);
            foreach (var user in msg.Users)
            {
                request.UserID = user.ID;
                var response = await request.ExecuteAsync();

                if (response.Error.ErrorType != VKErrors.None) notAll = true;
                else
                    Conversation.Users.Add(new VKProfileChat
                    {
                        ID = user.ID,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Photo50 = user.Photo50,
                        Sex = user.Sex,
                        Online = user.Online,
                        InvitedByID = ServiceHelper.SettingsService.AccessToken.UserID
                    });

                await Task.Delay(200);
            }

            if (notAll)
                await ServiceHelper.DialogService.ShowMessage("Один или несколько пользователей не были добавлены в чат.",
                    "Ошибка при добавлении");

            IsWork = false;       
        }
        #endregion
    }
}
