using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Windows.UI.Xaml;
using OneVK.Helpers;
using OneVK.Core;
using OneVK.Enums.App;
using OneVK.Model.Profile;
using Microsoft.Practices.ServiceLocation;
using OneVK.Core.Messages;
using OneVK.Service.Messages;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления диалога между двумя пользователями.
    /// </summary>
    public class DialogViewModel : BaseConversationViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public DialogViewModel(ulong userID)
            : base()
        {
            _userID = userID;
            OpenConversationAvatar = new RelayCommand(() =>
            {
                if (UserID < 1000000000) NavigationHelper.Navigate(AppViews.ProfileView, UserID);
                else NavigationHelper.Navigate(AppViews.GroupInfoView, UserID - 1000000000);
            });         
        }
        #endregion

        #region Приватные поля
        private ulong _userID;
        #endregion

        #region Свойства

        /// <summary>
        /// Возвращает идентификатор пользователя.
        /// </summary>
        public override ulong UserID { get { return _userID; } }

        /// <summary>
        /// Возвращает идентификатор чата.
        /// Не поддерживается.
        /// </summary>
        public override uint ChatID
        {
            get
            {
                throw new NotSupportedException(
                    "Идентификатор чата не поддерживается в диалоге.");
            }
        }

        /// <summary>
        /// Является ли диалог чатом.
        /// </summary>
        public override bool IsChat { get { return false; } }
        #endregion

        #region Команды

        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override async void Activate(NavigationMode mode = NavigationMode.New)
        {
            Messages = new MessagesCollection(ContentState.Loading);
            RaisePropertyChanged(() => Messages);

            IDialog dialog = null;
            try
            {
                dialog = await ServiceLocator.Current.GetInstance<IMessagesCacheService>()
                    .GetConversation((long)UserID) as IDialog;
            }
            catch (Exception) { }

            if (dialog == null)
            {
                try
                {
                    dialog = await ServiceLocator.Current.GetInstance<IConversationsService>()
                        .GetDialog(UserID);
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

            Conversation = dialog;
            Messages = dialog.Messages;
            RaisePropertyChanged(() => Messages);
            SendMessageCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Деактивирует модель представления.
        /// </summary>
        public override async void Deactivate(NavigationMode mode = NavigationMode.New)
        {
            //await ServiceHelper.MessagesService.CacheConversation(Conversation.ID);
        }
        #endregion

        #region Приватные методы
        #endregion
    }
}
