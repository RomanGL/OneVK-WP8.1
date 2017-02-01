using OneVK.Helpers;
using OneVK.Model.Photo;
using OneVK.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneVK.ViewModel.Settings
{
    /// <summary>
    /// Представляет модель представления страницы настроек уведомлений.
    /// </summary>
    public class NotificationsSettingsViewModel : BaseViewModel
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public NotificationsSettingsViewModel()
        {
            _service = ServiceHelper.SettingsService;
        }
        #endregion

        #region Приватные поля
        private SettingsService _service;
        #endregion

        #region Свойства
        /// <summary>
        /// Включены ли Push-уведомления.
        /// </summary>
        public bool EnablePushNotifications
        {
            get { return _service.EnablePushNotifications; }
            set
            {
                _service.EnablePushNotifications = value;
                RaisePropertyChanged(() => EnablePushNotifications);
            }
        }

        /// <summary>
        /// Включен ли текст в Push-уведомлениях.
        /// </summary>
        public bool EnableTextInNotifications
        {
            get { return _service.EnableTextInNotifications; }
            set
            {
                _service.EnableTextInNotifications = value;
                RaisePropertyChanged(() => EnableTextInNotifications);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для личных сообщений.
        /// </summary>
        public bool EnablePushMsgs
        {
            get { return _service.EnablePushMsgs; }
            set
            {
                _service.EnablePushMsgs = value;
                RaisePropertyChanged(() => EnablePushMsgs);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для чатов.
        /// </summary>
        public bool EnablePushChats
        {
            get { return _service.EnablePushChats; }
            set
            {
                _service.EnablePushChats = value;
                RaisePropertyChanged(() => EnablePushChats);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для отметок "Мне нравится".
        /// </summary>
        public bool EnablePushLikes
        {
            get { return _service.EnablePushLikes; }
            set
            {
                _service.EnablePushLikes = value;
                RaisePropertyChanged(() => EnablePushLikes);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для репостов.
        /// </summary>
        public bool EnablePushReposts
        {
            get { return _service.EnablePushReposts; }
            set
            {
                _service.EnablePushReposts = value;
                RaisePropertyChanged(() => EnablePushReposts);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для новых записей на стене.
        /// </summary>
        public bool EnablePushWallPost
        {
            get { return _service.EnablePushWallPost; }
            set
            {
                _service.EnablePushWallPost = value;
                RaisePropertyChanged(() => EnablePushWallPost);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для новых комментариев.
        /// </summary>
        public bool EnablePushComments
        {
            get { return _service.EnablePushComments; }
            set
            {
                _service.EnablePushComments = value;
                RaisePropertyChanged(() => EnablePushComments);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для упоминаний пользователя.
        /// </summary>
        public bool EnablePushMentions
        {
            get { return _service.EnablePushMentions; }
            set
            {
                _service.EnablePushMentions = value;
                RaisePropertyChanged(() => EnablePushMentions);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для новых ответов.
        /// </summary>
        public bool EnablePushReplies
        {
            get { return _service.EnablePushReplies; }
            set
            {
                _service.EnablePushReplies = value;
                RaisePropertyChanged(() => EnablePushReplies);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для заявок в друзья.
        /// </summary>
        public bool EnablePushFriendsRequests
        {
            get { return _service.EnablePushFriendsRequests; }
            set
            {
                _service.EnablePushFriendsRequests = value;
                RaisePropertyChanged(() => EnablePushFriendsRequests);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для приглашений в группы.
        /// </summary>
        public bool EnablePushGroupsInvites
        {
            get { return _service.EnablePushGroupsInvites; }
            set
            {
                _service.EnablePushGroupsInvites = value;
                RaisePropertyChanged(() => EnablePushGroupsInvites);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для ближайших мероприятий.
        /// </summary>
        public bool EnablePushEventsSoon
        {
            get { return _service.EnablePushEventsSoon; }
            set
            {
                _service.EnablePushEventsSoon = value;
                RaisePropertyChanged(() => EnablePushEventsSoon);
            }
        }

        /// <summary>
        /// Включены ли Push-уведомления для дней рождения друзей.
        /// </summary>
        public bool EnablePushFriendsBirthday
        {
            get { return _service.EnablePushFriendsBirthday; }
            set
            {
                _service.EnablePushFriendsBirthday = value;
                RaisePropertyChanged(() => EnablePushFriendsBirthday);
            }
        }

        /// <summary>
        /// Включен ли звук сообщения в приложении.
        /// </summary>
        public bool EnableInAppSound
        {
            get { return _service.EnableInAppSound; }
            set
            {
                _service.EnableInAppSound = value;
                RaisePropertyChanged(() => EnableInAppSound);
            }
        }

        /// <summary>
        /// Включена ли вибрация сообщения в приложении.
        /// </summary>
        public bool EnableInAppVibration
        {
            get { return _service.EnableInAppVibration; }
            set
            {
                _service.EnableInAppVibration = value;
                RaisePropertyChanged(() => EnableInAppVibration);
            }
        }

        /// <summary>
        /// Включены ли всплывающие строки при сообщении в приложении.
        /// </summary>
        public bool EnableInAppPopup
        {
            get { return _service.EnableInAppPopup; }
            set
            {
                _service.EnableInAppPopup = value;
                RaisePropertyChanged(() => EnableInAppPopup);
            }
        }
        #endregion

        #region Команды
        #endregion

        #region Публичные методы
        #endregion

        #region Приватные методы
        #endregion
    }
}
