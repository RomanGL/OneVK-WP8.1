using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneVK.Core;
using OneVK.Core.Collections;
using OneVK.Model.Message;
using OneVK.Helpers;
using OneVK.Enums.Common;
using OneVK.Enums.LongPoll;
using OneVK.Enums.Message;
using OneVK.Model.LongPoll;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using OneVK.Request.Execute;
using OneVK.Request;
using OneVK.Enums.App;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы сообщений пользователя.
    /// </summary>
    public class MessagesViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public MessagesViewModel()
        {
            OpenConversationCommand = new RelayCommand<VKDialog>(dialog =>
            {
                NavigationHelper.Navigate(AppViews.ConversationView, dialog.IsChat ? -dialog.Message.ChatID : (long)dialog.Message.UserID);
            });
            RefreshCommand = new RelayCommand(() => Dialogs.Refresh());

#if DEBUG
            if (IsInDesignModeStatic)
            {
                Dialogs.Add(DesignDataHelper.GetReadedSentDialog());
                Dialogs.Add(DesignDataHelper.GetUnreadSentDialog());
                Dialogs.Add(DesignDataHelper.GetUnreadDialog());
                Dialogs.Add(DesignDataHelper.GetReadedDialog());
                Dialogs.Add(DesignDataHelper.GetReadedSentChatDialog());
                Dialogs.Add(DesignDataHelper.GetUnreadSentChatDialog());
                Dialogs.Add(DesignDataHelper.GetUnreadChatDialog());
                Dialogs.Add(DesignDataHelper.GetReadedChatDialog());
            }
#endif
        }
        #endregion

        #region Приватные поля
        #endregion

        #region Свойства
        /// <summary>
        /// Коллекция диалогов.
        /// </summary>
        public DialogsCollection Dialogs { get { return ServiceHelper.MessagesService.Dialogs; } }
        #endregion

        #region Команды
        /// <summary>
        /// Комманда открытия беседы.
        /// </summary>
        public RelayCommand<VKDialog> OpenConversationCommand { get; private set; }
        /// <summary>
        /// Команда обновления списка диалогов.
        /// </summary>
        public RelayCommand RefreshCommand { get; private set; }
        #endregion

        #region Публичные методы
        /// <summary>
        /// активирует модель представления.
        /// </summary>
        public override async void Activate(NavigationMode mode = NavigationMode.New)
        {
            await Dialogs.Update();
        }        

        /// <summary>
        /// Деактивирует модель представления.
        /// </summary>
        public override void Deactivate(NavigationMode mode = NavigationMode.New)
        {
        }
        #endregion

        #region Приватные методы

        #endregion
    }
}
