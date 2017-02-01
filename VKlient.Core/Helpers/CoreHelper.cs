using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OneVK.Core;
using OneVK.Core.Transfer;
using OneVK.Enums.App;
using OneVK.Model.Video;
using OneVK.Service;
using OneVK.Service.Messages;
using OneVK.ViewModel;
using OneVK.ViewModel.Messages;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using VKSaver.Commands.SDK;
using Windows.Graphics.Display;
using Windows.Security.Cryptography;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System.Profile;
using Windows.UI.Core;

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет помощник функций ядра.
    /// </summary>
    public static class CoreHelper
    {
        /// <summary>
        /// Представляет диспетчер сообщений пользовательского интерфейса.
        /// </summary>
        public static CoreDispatcher Dispatcher { get; set; }

        /// <summary>
        /// Возвращает экземпляр локатора множественных экземпляров моделей представления.
        /// </summary>
        public static MultipleLocator MultipleLocator { get { return ServiceLocator.Current.GetInstance<MultipleLocator>(); } }

        /// <summary>
        /// Возвращает уникальный идентификатор устройства.
        /// </summary>
        public static string GetUniqueDeviceID()
        {
            return CryptographicBuffer.EncodeToBase64String(HardwareIdentification.GetPackageSpecificToken(null).Id).Replace("/", "");
        }

        /// <summary>
        /// Возвращает модель устройства.
        /// </summary>
        public static string GetDeviceName()
        {
            var info = new EasClientDeviceInformation();
            return info.SystemManufacturer + " " + info.SystemProductName;
        }

        /// <summary>
        /// Возвращает операционную систему устройства.
        /// </summary>
        public static string GetOperatingSystemName()
        {
            return (new EasClientDeviceInformation()).OperatingSystem;
        }

        /// <summary>
        /// Возвращает метку множественности числа.
        /// 1 - именительный.
        /// 2 - родительный.
        /// 3 - множественнный.
        /// </summary>
        /// <param name="number">Число.</param>
        public static byte GetNumberMark(int number)
        {
            if (Regex.Match(number.ToString(), "1\\d$").Success)
                return 3;
            if (Regex.Match(number.ToString(), "1$").Success)
                return 1;
            if (Regex.Match(number.ToString(), "(2|3|4)$").Success)
                return 2;
            return 3;
        }

        /// <summary>
        /// Инициализировать приложение.
        /// </summary>
        public static void Initialize(string tileId, bool restored = false)
        {
            if (restored)
            {
                (ServiceLocator.Current.GetInstance<SidebarViewModel>()).Activate();
                ServiceHelper.VKLongPollService.StartLongPolling();
                ServiceLocator.Current.GetInstance<IAppMessagesService>().StartAndRestore();
                ServiceLocator.Current.GetInstance<IPromoService>().Update();
                ProcessTile(tileId);
                return;
            }
            
            var message = new NavigateToPageMessage { Operation = NavigationType.New };
            if (ServiceLocator.Current.GetInstance<SettingsService>().AccessToken == null)
            {
                message.Page = AppViews.LoginView;
            }
            else
            {
                message.Page = AppViews.NewsView;
                (ServiceLocator.Current.GetInstance<SidebarViewModel>()).Activate();
                ServiceHelper.VKLongPollService.StartLongPolling();
                ServiceLocator.Current.GetInstance<IAppMessagesService>().StartAndRestore();
                ServiceLocator.Current.GetInstance<IPromoService>().Update();
            }

            if (!ProcessTile(tileId)) Messenger.Default.Send(message);
        }

        private static bool ProcessTile(string tileId)
        {
            if (String.IsNullOrEmpty(tileId)) return false;

            if (tileId.StartsWith("conv"))
            {
                NavigationHelper.Navigate(AppViews.ConversationView, long.Parse(tileId.Remove(0, 4)));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Возвращает ключ модели представления страницы настроек чата для указанного идентификатора.
        /// </summary>
        /// <param name="chatID">Идентификатор чата.</param>
        public static string GetChatSettingsUniqueViewModelKey(uint chatID) { return "chat_settings_" + chatID; }

        /// <summary>
        /// Возвращает ключ модели представления страницы беседы для указанного идентификатора.
        /// </summary>
        /// <param name="id">Идентификатор беседы.</param>
        public static string GetConversationUniqueViewModelKey(long id) { return "conv" + id; }

        /// <summary>
        /// Возвращает ключ модели представления страницы пользователя для указанного идентификатора пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        public static string GetProfileViewModelKey(ulong userID) { return "profile" + userID; }

        /// <summary>
        /// Возвращает ключ модели представления страницы сообщества для указанного идентификатора сообщества.
        /// </summary>
        /// <param name="group">Идентификатор сообщества.</param>
        public static string GetGroupInfoViewModelKey(ulong groupID) { return "group" + groupID; }

        /// <summary>
        /// Возвращает ключ модели представления для указанного идентификатора поста и отправителя.
        /// </summary>
        /// <param name="postID">Идентификатор поста.</param>
        /// <param name="ownerID">Идентификатор отправителя.</param>
        public static string GetPostViewModelKey(ulong postID, long ownerID) { return string.Format("post{0}_{1}", postID, ownerID); }

        /// <summary>
        /// Возвращает ключ модели представления для указанного идентификатора видеоальбома и его владельца.
        /// </summary>
        /// <param name="albumID">Идентификатор видеоальбома.</param>
        /// <param name="ownerID">Идентификатор владельца.</param>
        public static string GetVideoAlbumViewModelKey(long albumID, long ownerID) { return string.Format("videoalbum{0}_{1}", albumID, ownerID); }

        /// <summary>
        /// Возвращает ключ модели представления страницы аудио для указанного идентификатора владельца.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца аудиозаписей.</param>
        public static string GetAudiosViewModelKey(long ownerID) { return "audios" + ownerID; }

        /// <summary>
        /// Возвращает ключ модели представления страницы видео для указанного идентификатора владельца.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца видеозаписей.</param>
        public static string GetVideosViewModelKey(long ownerID) { return "videos" + ownerID; }

        /// <summary>
        /// Возвращает ключ модели представления страницы друзей для указанного идентификатора пользователя.
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        public static string GetFriendsViewModelKey(ulong userID) { return "friends" + userID; }

        /// <summary>
        /// Возвращает ключ модели представления страницы видео для указанных идентификатора видеозаписи и идентификатора её владельца.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца видеозаписи.</param>
        /// <param name="videoID">Идентификатор владельца видеозаписи.</param>
        public static string GetVideoInfoViewModelKey(long ownerID, ulong videoID) { return string.Format("video{0}_{1}", ownerID, videoID); }

        /// <summary>
        /// Возвращает ключ модели представления страницы фотографий для указанного идентификатора владельца.
        /// </summary>
        /// <param name="ownerID">Идентификатор владельца фотографий.</param>
        public static string GetPhotosViewModelKey(long ownerID) { return "photos" + ownerID; }

        /// <summary>
        /// Возвращает ключ модели представления страницы просмотра фотографий.
        /// </summary>
        /// <param name="firstPhotoID">Идентификатор первой фотографии.</param>
        /// <param name="ownerID">Идентификатор владельца первой фотографии.</param>
        public static string GetImageViewModelKey(ulong firstPhotoID, long ownerID) { return string.Format("images{0}_{1}", firstPhotoID, ownerID); }

        /// <summary>
        /// Блокирует ориентацию на портретную.
        /// </summary>
        public static void LockOrientation()
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
        }

        /// <summary>
        /// Разблокирует смену ориентации экрана.
        /// </summary>
        public static void UnlockOrientation()
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
        }  
        
        /// <summary>
        /// Отправляет внутренее всплывающее уведомление.
        /// </summary>
        /// <param name="content">Содержимое уведомления.</param>
        /// <param name="title">Заголовок уведомления.</param>
        /// <param name="type">Тип уведомления.</param>
        /// <param name="imageUrl">Путь к картинке уведомления.</param>
        /// <param name="duration">Длительность уведомления.</param>
        /// <param name="parameter">Параметр навигации для уведомления.</param>
        /// <param name="actionToDo">Делегат для выполнения при нажатии уведомления.</param>
        public static void SendInAppPush(string content, string title = null, PopupMessageType type = PopupMessageType.Default,
            string imageUrl = null, TimeSpan duration = default(TimeSpan), NavigateToPageMessage parameter = null, Action actionToDo = null)
        {
            var pop = new PopupMessage()
            {
                Title = title,
                Content = content,
                Type = type,
                ImageUrl = imageUrl,
                Parameter = parameter,
                ActionToDo = actionToDo
            };
            if (duration != default(TimeSpan))
                pop.Duration = duration;

            Messenger.Default.Send(pop);
        }

        /// <summary>
        /// Возвращает объект загрузки, совместимый с ВКачай.
        /// </summary>
        /// <param name="obj">Объект для загрузки.</param>
        public static VKSaverDownload GetDownload(IDownloadSupport obj)
        {
            return new VKSaverDownload
            {
                ContentType = (VKSaver.Commands.SDK.FileContentType)obj.ContentType,
                Extension = obj.Extension,
                FileName = obj.FileName,
                Source = obj.Source
            };
        }
    }
}
