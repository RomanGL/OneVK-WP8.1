using GalaSoft.MvvmLight.Command;
using OneVK.Core.Collections;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Модель представления списка друзей.
    /// </summary>
    public class FriendsViewModel : BaseKeyedViewModel<FriendsViewModel>
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public FriendsViewModel(string uniqueKey, ulong userID)
            : base(uniqueKey)
        {
            _friends = new FriendsCollection(userID);
            _friendsOnline = new FriendsOnlineCollection(userID);
            _friendsList = new FriendsListCollection(userID);
            RefreshCommand = new RelayCommand(() => Friends.Refresh());
            RefreshOnlineCommand = new RelayCommand(() => FriendsOnline.Refresh());
            RefreshListCommand = new RelayCommand(() => FriendsList.Refresh());
        }
        #endregion

        #region Приватные поля
        private FriendsCollection _friends;
        private FriendsOnlineCollection _friendsOnline;
        private FriendsListCollection _friendsList;
        #endregion

        #region Свойства
        /// <summary>
        /// Обозреваемая коллекция друзей.
        /// </summary>
        public FriendsCollection Friends { get { return _friends; } }
        /// <summary>
        /// Обозреваемая коллекция друзей онлайн.
        /// </summary>
        public FriendsOnlineCollection FriendsOnline { get { return _friendsOnline; } }
        /// <summary>
        /// Обозреваемая коллекция списков друзей.
        /// </summary>
        public FriendsListCollection FriendsList { get { return _friendsList; } }
        #endregion

        #region Команды
        /// <summary>
        /// Комманда обновления списка друзей.
        /// </summary>
        public RelayCommand RefreshCommand { get; private set; }
        /// <summary>
        /// Комманда обновления списка друзей онлайн.
        /// </summary>
        public RelayCommand RefreshOnlineCommand { get; private set; }
        /// <summary>
        /// Комманда обновления списков друзей.
        /// </summary>
        public RelayCommand RefreshListCommand { get; private set; }

        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion       
    }
}
