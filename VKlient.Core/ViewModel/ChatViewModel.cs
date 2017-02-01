using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OneVK.Model.Message;
using OneVK.Request;
using OneVK.Enums.Common;
using OneVK.Enums.Message;
using System.Collections.ObjectModel;
using OneVK.Core.Collections;
using GalaSoft.MvvmLight.Command;
using OneVK.Model.LongPoll;
using OneVK.Enums.LongPoll;
using OneVK.Core;
using OneVK.Helpers;
using OneVK.Model.Profile;
using OneVK.Enums.App;
using Newtonsoft.Json;
using Microsoft.Practices.ServiceLocation;
using OneVK.Service.Messages;
using OneVK.Core.Messages;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления чата.
    /// </summary>
    public class ChatViewModel : BaseConversationViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public ChatViewModel(uint chatID)
            : base()
        {
            _chatID = chatID;
            OpenChatSettings = new RelayCommand(() =>
            {
                NavigationHelper.Navigate(AppViews.ChatSettingsView, ChatID);
            });
            OpenConversationAvatar = OpenChatSettings;
        }
        #endregion

        #region Приватные поля
        private uint _chatID;
        #endregion

        #region Команды
        /// <summary>
        /// Команда открытия настроек чата.
        /// </summary>
        public RelayCommand OpenChatSettings { get; private set; }
        #endregion

        #region Свойства

        /// <summary>
        /// Возвращает значение, что это модель представления чата.
        /// </summary>
        public override bool IsChat { get { return true; } }

        /// <summary>
        /// Возвращает идентификатор пользователя, с которым ведется диалог.
        /// Не поддерживается в чате.
        /// </summary>
        public override ulong UserID
        {
            get 
            { 
                throw new NotSupportedException(
                    "Идентификатор пользователя не поддерживается в чате."); 
            }
        }

        /// <summary>
        /// Возвращает идентификатор чата.
        /// </summary>
        public override uint ChatID { get { return _chatID; } }
        #endregion
        
        #region Публичные методы

        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override async void Activate(NavigationMode mode = NavigationMode.New)
        {
            Messages = new MessagesCollection(ContentState.Loading);
            RaisePropertyChanged(() => Messages);

            IChat chat = null;
            try
            {
                chat = await ServiceLocator.Current.GetInstance<IMessagesCacheService>()
                    .GetConversation(-ChatID) as IChat;
            }
            catch (Exception) { }

            if (chat == null)
            {
                try
                {
                    chat = await ServiceLocator.Current.GetInstance<IConversationsService>()
                        .GetChat(ChatID);
                }
                catch (Exception ex)
                {
                    Messages = new MessagesCollection(ContentState.Error);
                    Messages.LoadCommand = new RelayCommand(() => Activate());

                    RaisePropertyChanged(() => Messages);
                    SendMessageCommand.RaiseCanExecuteChanged();
                    throw ex;
                }
            }

            Conversation = chat;
            Messages = chat.Messages;
            RaisePropertyChanged(() => Messages);
            SendMessageCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Деактивирует модель представления.
        /// </summary>
        public override void Deactivate(NavigationMode mode = NavigationMode.New)
        {
            //await ServiceHelper.MessagesService.CacheConversation(Conversation.ID);
        }
        #endregion

        #region Приватные методы
        #endregion        
    }
}
