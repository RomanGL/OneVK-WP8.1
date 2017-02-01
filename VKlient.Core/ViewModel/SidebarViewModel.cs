using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OneVK.Model.Profile;
using GalaSoft.MvvmLight;
using OneVK.Request;
using OneVK.Enums.Common;
using OneVK.ViewModel.Messages;
using GalaSoft.MvvmLight.Messaging;
using OneVK.Response;
using OneVK.Helpers;
using GalaSoft.MvvmLight.Command;
using OneVK.Enums.App;
using Windows.UI.Xaml.Navigation;

namespace OneVK.ViewModel
{
    public class SidebarViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public SidebarViewModel()
        {
#if DEBUG
            if (IsInDesignModeStatic)
            {
                Profile = new VKProfileBase
                {
                    FirstName = "Роман",
                    LastName = "Гладких",
                    Status = "OneVK design mode."
                };
                MessagesCount = 159;
            }
#endif
            GoToMessagesView = new RelayCommand(() => NavigationHelper.Navigate(AppViews.MessagesView));

            ServiceHelper.VKLongPollService.MessagesCounterChanged += (s, e) =>
            {
                MessagesCount = e.Count;
                RaisePropertyChanged("HasNotifications");
            };
            Messenger.Default.Register<VKAccountGetCountersResponse>(this, OnCountersMessageReceived);
        }
        #endregion

        #region Приватные поля
        private VKProfileBase _profile;
        private byte _retriesCount;
        private int _messagesCount;
        private int _friendsCount;
        private int _groupsCount;
        private int _feedbacksCount;
        private int _videosCount;
        private int _eventsCount;
        private int _notesCount;
        private int _photosCount;
        #endregion

        #region Свойства
        /// <summary>
        /// Команда перехода в представление мообщений.
        /// </summary>
        public RelayCommand GoToMessagesView { get; private set; }

        /// <summary>
        /// Имеются ли уведомления.
        /// </summary>
        public bool HasNotifications
        {
            get
            {
                return MessagesCount > 0 || FeedbacksCount > 0 ||
                    FriendsCount > 0 || GroupsCount > 0 ||
                    VideosCount > 0 || PhotosCount > 0 ||
                    EventsCount > 0 || NotesCount > 0;
            }
        }
        /// <summary>
        /// Количество новых фотографий.
        /// </summary>
        public int PhotosCount
        {
            get { return _photosCount; }
            private set { Set(() => PhotosCount, ref _photosCount, value); }
        }
        /// <summary>
        /// Количество новых заметок.
        /// </summary>
        public int NotesCount
        {
            get { return _notesCount; }
            private set { Set(() => NotesCount, ref _notesCount, value); }
        }
        /// <summary>
        /// Количество новых событий.
        /// </summary>
        public int EventsCount
        {
            get { return _eventsCount; }
            private set { Set(() => EventsCount, ref _eventsCount, value); }
        }
        /// <summary>
        /// Количество новых видеозаписей.
        /// </summary>
        public int VideosCount
        {
            get { return _videosCount; }
            private set { Set(() => VideosCount, ref _videosCount, value); }
        }
        /// <summary>
        /// Количество новых ответов.
        /// </summary>
        public int FeedbacksCount
        {
            get { return _feedbacksCount; }
            private set { Set(() => FeedbacksCount, ref _feedbacksCount, value); }
        }
        /// <summary>
        /// Количество новых приглашений в сообщества.
        /// </summary>
        public int GroupsCount
        {
            get { return _groupsCount; }
            private set { Set(() => GroupsCount, ref _groupsCount, value); }
        }
        /// <summary>
        /// Количество новых заявок в друзья.
        /// </summary>
        public int FriendsCount
        {
            get { return _friendsCount; }
            private set { Set(() => FriendsCount, ref _friendsCount, value); }
        }
        /// <summary>
        /// Количество новых сообщений.
        /// </summary>
        public int MessagesCount
        {
            get { return _messagesCount; }
            private set { Set(() => MessagesCount, ref _messagesCount, value); }
        }

        /// <summary>
        /// Профиль текущего пользователя.
        /// </summary>
        public VKProfileBase Profile
        {
            get { return _profile; }
            private set { Set(() => Profile, ref _profile, value); }
        }
        #endregion

        #region Команды
        #endregion

        #region Публичные методы
        /// <summary>
        /// Активирует модель представления.
        /// </summary>
        public override async void Activate(NavigationMode mode = NavigationMode.New)
        {
            _retriesCount++;
            var response = await (new GetUsersRequest()).ExecuteAsync();
            if (response.Error.ErrorType == VKErrors.None)
            {
                Profile = response.Response[0];
                _retriesCount = 0;
            }
            else if (_retriesCount <= 5)
                Activate();
            else
                _retriesCount = 0;
        }

        /// <summary>
        /// Деактивирует модель представления.
        /// </summary>
        public override void Deactivate(NavigationMode mode = NavigationMode.New)
        {
            Profile = null;
        }
        #endregion

        #region Приватные методы
        /// <summary>
        /// Вызывается при получении нового сообщения
        /// </summary>
        /// <param name="message">Сообщение.</param>
        private void OnCountersMessageReceived(VKAccountGetCountersResponse message)
        {
            MessagesCount = message.Messages;
            FriendsCount = message.Friends;
            FeedbacksCount = message.Notifications;
            GroupsCount = message.Groups;
            VideosCount = message.Videos;
            PhotosCount = message.Photos;
            EventsCount = message.Events;
            NotesCount = message.Notes;

            RaisePropertyChanged("HasNotifications");
        }
        #endregion
    }
}
