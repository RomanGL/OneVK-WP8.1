using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.Model.Common;
using OneVK.Model.Photo;
using System;
using System.Collections.Generic;

namespace OneVK.Service
{
    /// <summary>
    /// Представляет собой сервис для получения
    /// и сохранения параметров.
    /// </summary>
    public class SettingsService
    {
        private Dictionary<string, object> _cachedSettings = new Dictionary<string, object>();

        /// <summary>
        /// Возвращает или задает максимальный размер загружаемых изображений.
        /// </summary>
        public VKPhotoSizes MaxPhotosSize
        {
            get { return GetValue(AppConstants.MaxPhotosSize, VKPhotoSizes.Photo604); }
            set { SetValue(AppConstants.MaxPhotosSize, value); }
        }

        /// <summary>
        /// Идентификатор текущего трека.
        /// </summary>
        public int CurrentTrackID { get { return SettingsHelper.Get<int>(AppConstants.PlayerCurrentTrackID); } }

        /// <summary>
        /// Запущена ли фоновая задача воспроизведения аудио.
        /// </summary>
        public bool IsPlayerTaskRunning
        {
            get { return SettingsHelper.Get<bool>(AppConstants.PlayerTaskRunning); }
        }

        /// <summary>
        /// Возвращает или задает режим повтора воспроизведения в плеере.
        /// </summary>
        public bool PlayerRepeatMode
        {
            get { return GetValue(AppConstants.PlayerRepeatMode, false); }
            set { SetValue(AppConstants.PlayerRepeatMode, value); }
        }

        /// <summary>
        /// Возвращает или задает случайный режим воспроизведения в плеере.
        /// </summary>
        public bool PlayerShuffleMode
        {
            get { return GetValue(AppConstants.PlayerShuffleMode, false); }
            set { SetValue(AppConstants.PlayerShuffleMode, value); }
        }

        /// <summary>
        /// Возвращает или задает ключ доступа ВКонтакте.
        /// </summary>
        public VKAccessToken AccessToken
        {
            get { return GetValue<VKAccessToken>(AppConstants.AccessToken); }
            set { SetValue(AppConstants.AccessToken, value); }
        }

        /// <summary>
        /// Возвращает или задает значение режима невидимости.
        /// </summary>
        public bool StayOfflineMode
        {
            get { return GetValue(AppConstants.StayOfflineMode, false); }
            set { SetValue(AppConstants.StayOfflineMode, value); }
        }

        /// <summary>
        /// Возвращает или задает значение активности режима кэширования музыки.
        /// </summary>
        public bool EnableMusicCache
        {
            get { return GetValue(AppConstants.EnableMusicCache, true); }
            set { SetValue(AppConstants.EnableMusicCache, value); }
        }

        /// <summary>
        /// Возвращает или задает значение активности режима кэширования изображений исполнителей.
        /// </summary>
        public bool EnableCacheArtistsImages
        {
            get { return GetValue(AppConstants.EnableCacheArtistsImages, true); }
            set { SetValue(AppConstants.EnableCacheArtistsImages, value); }
        }

        /// <summary>
        /// Возвращает или задает значение активности режима кэширования изображений исполнителей только через Wi-Fi.
        /// </summary>
        public bool EnableCacheArtistsImagesOnlyFromWiFi
        {
            get { return GetValue(AppConstants.EnableCacheArtistsImagesOnlyFromWiFi, false); }
            set { SetValue(AppConstants.EnableCacheArtistsImagesOnlyFromWiFi, value); }
        }

        #region Notifications

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений.
        /// </summary>
        public bool EnablePushNotifications
        {
            get { return GetValue(AppConstants.EnablePushNotifications, true); }
            set { SetValue(AppConstants.EnablePushNotifications, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения текста в Push-уведомлениях.
        /// </summary>
        public bool EnableTextInNotifications
        {
            get { return GetValue(AppConstants.EnableTextInNotifications, true); }
            set { SetValue(AppConstants.EnableTextInNotifications, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для личных сообщений.
        /// </summary>
        public bool EnablePushMsgs
        {
            get { return GetValue(AppConstants.EnablePushMsgs, true); }
            set { SetValue(AppConstants.EnablePushMsgs, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для сообщений в чатах.
        /// </summary>
        public bool EnablePushChats
        {
            get { return GetValue(AppConstants.EnablePushChats, true); }
            set { SetValue(AppConstants.EnablePushChats, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для отметок Мне нравится.
        /// </summary>
        public bool EnablePushLikes
        {
            get { return GetValue(AppConstants.EnablePushLikes, true); }
            set { SetValue(AppConstants.EnablePushLikes, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для репостов записей.
        /// </summary>
        public bool EnablePushReposts
        {
            get { return GetValue(AppConstants.EnablePushReposts, true); }
            set { SetValue(AppConstants.EnablePushReposts, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для записей на стене пользователя.
        /// </summary>
        public bool EnablePushWallPost
        {
            get { return GetValue(AppConstants.EnablePushWallPost, true); }
            set { SetValue(AppConstants.EnablePushWallPost, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для новых комментариев.
        /// </summary>
        public bool EnablePushComments
        {
            get { return GetValue(AppConstants.EnablePushComments, true); }
            set { SetValue(AppConstants.EnablePushComments, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для упоминаний пользователя.
        /// </summary>
        public bool EnablePushMentions
        {
            get { return GetValue(AppConstants.EnablePushMentions, true); }
            set { SetValue(AppConstants.EnablePushMentions, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для ответов.
        /// </summary>
        public bool EnablePushReplies
        {
            get { return GetValue(AppConstants.EnablePushReplies, true); }
            set { SetValue(AppConstants.EnablePushReplies, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для заявок в друзья.
        /// </summary>
        public bool EnablePushFriendsRequests
        {
            get { return GetValue(AppConstants.EnablePushFriendsRequests, true); }
            set { SetValue(AppConstants.EnablePushFriendsRequests, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для приглашений в группы.
        /// </summary>
        public bool EnablePushGroupsInvites
        {
            get { return GetValue(AppConstants.EnablePushGroupsInvites, true); }
            set { SetValue(AppConstants.EnablePushGroupsInvites, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для ближайших мероприятий.
        /// </summary>
        public bool EnablePushEventsSoon
        {
            get { return GetValue(AppConstants.EnablePushEventsSoon, true); }
            set { SetValue(AppConstants.EnablePushEventsSoon, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения Push-уведомлений для дней рождения друзей.
        /// </summary>
        public bool EnablePushFriendsBirthday
        {
            get { return GetValue(AppConstants.EnablePushFriendsBirthday, true); }
            set { SetValue(AppConstants.EnablePushFriendsBirthday, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения звука сообщения в приложении.
        /// </summary>
        public bool EnableInAppSound
        {
            get { return GetValue(AppConstants.EnableInAppSound, true); }
            set { SetValue(AppConstants.EnableInAppSound, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения вибрации сообщения в приложении.
        /// </summary>
        public bool EnableInAppVibration
        {
            get { return GetValue(AppConstants.EnableInAppVibration, true); }
            set { SetValue(AppConstants.EnableInAppVibration, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения всплывающей строки сообщения в приложении.
        /// </summary>
        public bool EnableInAppPopup
        {
            get { return GetValue(AppConstants.EnableInAppPopup, true); }
            set { SetValue(AppConstants.EnableInAppPopup, value); }
        }

        /// <summary>
        /// Возвращает или задает значение включения рекламных акций в приложении.
        /// </summary>
        public bool EnablePromo
        {
            get { return GetValue(AppConstants.EnablePromo, true); }
            set { SetValue(AppConstants.EnablePromo, value); }
        }

        /// <summary>
        /// Возвращает или задает тип используемых Emoji-символов.
        /// </summary>
        public EmojiType EmojiType
        {
            get { return GetValue(AppConstants.EmojiType, EmojiType.Twitter); }
            set { SetValue(AppConstants.EmojiType, value); }
        }

        #endregion

        /// <summary>
        /// Возвращает указанный параметр из кэша. При отсутствии в кэше и в сохраненных 
        /// параметрах создает новый с указанным значением по умолчанию.
        /// </summary>
        /// <typeparam name="T">Тип объекта параметра.</typeparam>
        /// <param name="settingName">Имя параметра.</param>
        /// <param name="defaultValue">Станджартное значение параметра.</param>
        public T GetValue<T>(string settingName, T defaultValue = default(T))
        {
            if (_cachedSettings.ContainsKey(settingName))
                return (T)_cachedSettings[settingName];

            T setting;
            if (!SettingsHelper.ContainsSetting(settingName))
            {
                setting = defaultValue;                
                SettingsHelper.Set<T>(settingName, setting);                
            }
            else
                setting = SettingsHelper.Get<T>(settingName);

            _cachedSettings[settingName] = setting;
            return setting;
        }

        /// <summary>
        /// Записывает указанный параметр в кэш и на диск.
        /// </summary>
        /// <typeparam name="T">Тип объекта параметра.</typeparam>
        /// <param name="settingName">Имя параметра.</param>
        /// <param name="value">Новое значение параметра.</param>
        public void SetValue<T>(string settingName, T value)
        {
            _cachedSettings[settingName] = value;
            SettingsHelper.Set<T>(settingName, value);
        }
    }
}
