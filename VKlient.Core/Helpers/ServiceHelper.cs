#if ONEVK_CORE
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
#endif
using OneVK.Service;
using OneVK.ViewModel;

namespace OneVK.Helpers
{
    /// <summary>
    /// Представляет помощник по работе с сервисами.
    /// </summary>
    public static class ServiceHelper
    {
#if ONEVK_CORE
        /// <summary>
        /// Возвращает текущий экземпляр сервиса для работы с базовыми функциями приложения.
        /// </summary>
        public static CoreService CoreService { get { return ServiceLocator.Current.GetInstance<CoreService>(); } }

        /// <summary>
        /// Возвращает текущий экземпляр сервиса диалоговых окон.
        /// </summary>
        public static IDialogService DialogService { get { return ServiceLocator.Current.GetInstance<IDialogService>(); } }

        /// <summary>
        /// Возвращает текущий экземпляр сервиса кэширования сообщений.
        /// </summary>
        internal static MessagesCacheServiceOld MessagesCacheService { get { return ServiceLocator.Current.GetInstance<MessagesCacheServiceOld>(); } }

        /// <summary>
        /// Возвращает текущий экземпляр внутреннего сервиса для работы с сообщениями.
        /// </summary>
        internal static MessagesService MessagesService { get { return ServiceLocator.Current.GetInstance<MessagesService>(); } }
#endif
        /// <summary>
        /// Возвращает текущий экземпляр сервиса для работы с друзьями ВКонтакте.
        /// </summary>
        public static VKFriendsService VKFriendsService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VKFriendsService>();
            }
        }

        /// <summary>
        /// Возвращает текущий экземпляр сервиса для работы с документами ВКонтакте.
        /// </summary>
        public static VKDocumentsService VKDocumentService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VKDocumentsService>();
            }
        }

        /// <summary>
        /// Возвращает текущий экземпляр сервиса для авторизации ВКонтакте.
        /// </summary>
        public static VKLoginService VKLoginService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VKLoginService>();
            }
        }

        /// <summary>
        /// Возвращает текущий экземпляр сервиса работы с LongPoll-сервером ВКонтакте.
        /// </summary>
        public static VKLongPollService VKLongPollService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VKLongPollService>();
            }
        }

        /// <summary>
        /// Возвращает текущий экземпляр сервиса для работы с параметрами.
        /// </summary>
        public static SettingsService SettingsService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsService>();
            }
        }

        /// <summary>
        /// Возвращает текущий экземпляр сервиса воспроизведения.
        /// </summary>
        public static IPlayerService PlayerService { get { return ServiceLocator.Current.GetInstance<IPlayerService>(); } }
        
        /// <summary>
        /// Возвращает текущий экземпляр сервиса работы с Xbox Music.
        /// </summary>
        public static XboxMusicService XboxMusicService { get { return ServiceLocator.Current.GetInstance<XboxMusicService>(); } }

        /// <summary>
        /// Возвращает текущий экземпляр локатора моделей по ключу.
        /// </summary>
        public static KeyedViewModelLocator KeyedLocator { get { return ServiceLocator.Current.GetInstance<KeyedViewModelLocator>(); } }
    }
}
