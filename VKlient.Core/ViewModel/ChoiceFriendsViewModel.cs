using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OneVK.Core.Collections;
using OneVK.Helpers;
using OneVK.Model.Profile;
using OneVK.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет модель представления страницы выбора друзей.
    /// </summary>
    public class ChoiceFriendsViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ChoiceFriendsViewModel"/>.
        /// </summary>
        public ChoiceFriendsViewModel()
        {
            AddUsers = new RelayCommand(() =>
            {
                if (CurrentChatID == 0)
                    throw new InvalidOperationException("Невозможно добавить пользователей в неопределенный чат.");

                Messenger.Default.Send(new AddFriendsToChatMessage { ChatID = CurrentChatID, Users = SelectedUsers }, CurrentChatID);
                SelectedUsers = null;
                NavigationHelper.GoBack();
            }, () => SelectedUsers != null && SelectedUsers.Count > 0);
        }
        #endregion

        #region Приватные поля
        #endregion

        #region Свойства
        /// <summary>
        /// Идентификатор текущего чата.
        /// </summary>
        public static uint CurrentChatID { get; set; }

        /// <summary>
        /// Возвращает коллекцию друзей.
        /// </summary>
        public FriendsCollection Friends
        {
            get
            {
                string uniqueKey = CoreHelper.GetFriendsViewModelKey(0);
                var vm = ServiceHelper.KeyedLocator.GetByKey(uniqueKey, () => new FriendsViewModel(uniqueKey, 0));
                return vm.Friends;
            }
        }

        /// <summary>
        /// Возвращает или задает список выделенных пользователей.
        /// </summary>
        public List<VKProfileShort> SelectedUsers { get; set; }
        #endregion

        #region Команды
        /// <summary>
        /// Команда добавления пользователей в чат.
        /// </summary>
        public RelayCommand AddUsers { get; private set; }
        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion
    }
}
