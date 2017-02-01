using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using OneVK.Service;
using OneVK.Helpers;
using OneVK.Model.Photo;
using System.Collections.Generic;
using System;
using OneVK.ViewModel.Settings;
using OneVK.Service.Messages;
using OneVK.Service.Common;

namespace OneVK.ViewModel
{
    /// <summary>
    /// Представляет локатор моделей представления и сервисов в одном экземпляре.
    /// </summary>
    public class SinglelLocator
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public SinglelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
            }
            else
            {
                SimpleIoc.Default.Register<VKLoginService>(true);
                SimpleIoc.Default.Register<VKUserV1Service>();
                SimpleIoc.Default.Register<VKOldPhotosService>();
                SimpleIoc.Default.Register<VKDocumentsService>();
                SimpleIoc.Default.Register<SettingsService>();
                SimpleIoc.Default.Register<VKLongPollService>();
                SimpleIoc.Default.Register<CoreService>(true);
                SimpleIoc.Default.Register<XboxMusicService>();
                SimpleIoc.Default.Register<MessagesCacheServiceOld>();
                SimpleIoc.Default.Register<MessagesService>(true);

                SimpleIoc.Default.Register<IMessagesCacheService, MessagesCacheService>();
                SimpleIoc.Default.Register<IMessagesUsersService, MessagesUserService>();
                SimpleIoc.Default.Register<IConversationsService, ConversatonsService>();
                SimpleIoc.Default.Register<IDialogsCacheService, DialogsCacheService>();
                SimpleIoc.Default.Register<IAppMessagesService, AppMessagesService>();
                SimpleIoc.Default.Register<ILogService, DebugLogService>();
                SimpleIoc.Default.Register<IPromoService, PromoService>();
            }

            SimpleIoc.Default.Register<MultipleLocator>();
            SimpleIoc.Default.Register<KeyedViewModelLocator>();

            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<NewsViewModel>();
            SimpleIoc.Default.Register<AudiosViewModel>();
            SimpleIoc.Default.Register<VideosViewModel>();
            SimpleIoc.Default.Register<PlayerViewModel>(true);
            SimpleIoc.Default.Register<VideoAlbumViewModel>();
            SimpleIoc.Default.Register<MessagesViewModel>();
            SimpleIoc.Default.Register<BotViewModel>();
            SimpleIoc.Default.Register<GroupsViewModel>();
            SimpleIoc.Default.Register<SidebarViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<PhotosViewModel>();
            SimpleIoc.Default.Register<FriendsViewModel>();
            SimpleIoc.Default.Register<ChangeStatusViewModel>();
            SimpleIoc.Default.Register<NewPostViewModel>();
            SimpleIoc.Default.Register<CommonSettingsViewModel>();
            SimpleIoc.Default.Register<NotificationsSettingsViewModel>();
            SimpleIoc.Default.Register<ChoiceFriendsViewModel>();
            SimpleIoc.Default.Register<AttachmentsManagerViewModel>();
            SimpleIoc.Default.Register<NotificationsViewModel>();
            SimpleIoc.Default.Register<AllPromosViewModel>();
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        /// <summary>
        /// Модель представления менеджера вложений.
        /// </summary>
        public AttachmentsManagerViewModel AttachmentsManagerVM
        {
            get { return ServiceLocator.Current.GetInstance<AttachmentsManagerViewModel>(); }
        }

        /// <summary>
        /// Модель представления всех рекламных акций.
        /// </summary>
        public AllPromosViewModel AllPromosVM
        {
            get { return ServiceLocator.Current.GetInstance<AllPromosViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы ответов.
        /// </summary>
        public NotificationsViewModel NotificationsVM
        {
            get { return ServiceLocator.Current.GetInstance<NotificationsViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы выбора друзей.
        /// </summary>
        public ChoiceFriendsViewModel ChoiceFriendsVM
        {
            get { return ServiceLocator.Current.GetInstance<ChoiceFriendsViewModel>(); }
        }

        /// <summary>
        /// Модель представления бота.
        /// </summary>
        public BotViewModel BotVM
        {
            get { return ServiceLocator.Current.GetInstance<BotViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы диалогов.
        /// </summary>
        public MessagesViewModel MessagesVM
        {
            get { return ServiceLocator.Current.GetInstance<MessagesViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы авторизации.
        /// </summary>
        public LoginViewModel LoginVM
        {
            get { return ServiceLocator.Current.GetInstance<LoginViewModel>(); }
        }

        /// <summary>
        /// Модель представления странцы новостей.
        /// </summary>
        public NewsViewModel NewsVM
        {
            get { return ServiceLocator.Current.GetInstance<NewsViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы просмотра записи (поста).
        /// </summary>
        public PostViewModel PostVM
        {
            get { return ServiceLocator.Current.GetInstance<PostViewModel>(); }
        }

        /// <summary>
        /// Модель представления боковой панели.
        /// </summary>
        public SidebarViewModel SidebarVM
        {
            get { return ServiceLocator.Current.GetInstance<SidebarViewModel>(); }
        }

        /// <summary>
        /// Возвращает модель представления страницы настроек.
        /// </summary>
        public SettingsViewModel SettingsVM
        {
            get { return ServiceLocator.Current.GetInstance<SettingsViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы проигрывателя.
        /// </summary>
        public PlayerViewModel PlayerVM
        {
            get { return ServiceLocator.Current.GetInstance<PlayerViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы смены статуса.
        /// </summary>
        public ChangeStatusViewModel ChangeStatusVM
        {
            get { return ServiceLocator.Current.GetInstance<ChangeStatusViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы создания нового поста.
        /// </summary>
        public NewPostViewModel NewPostVM
        {
            get { return ServiceLocator.Current.GetInstance<NewPostViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы общих настроек.
        /// </summary>
        public CommonSettingsViewModel CommonSettingsVM
        {
            get { return ServiceLocator.Current.GetInstance<CommonSettingsViewModel>(); }
        }

        /// <summary>
        /// Модель представления страницы настроек уведомлений.
        /// </summary>
        public NotificationsSettingsViewModel NotificationsSettingsVM
        {
            get { return ServiceLocator.Current.GetInstance<NotificationsSettingsViewModel>(); }
        }

#if DEBUG
        /// <summary>
        /// Возщвращает экземпляр модели представления страницы поста времени разработки.
        /// </summary>
        public PostViewModel PostDesignTimeVM
        {
            get { return new PostViewModel("design", DesignDataHelper.GetNewsfeedPost()); }
        }

        /// <summary>
        /// Возщвращает экземпляр модели представления страницы аудиозаписей времени разработки.
        /// </summary>
        public AudiosViewModel AudiosDesignTimeVM
        {
            get { return new AudiosViewModel("design", 0); }
        }

        /// <summary>
        /// Возвращает экземпляр модели представления страницы фотографий времени разработки.
        /// </summary>
        public PhotosViewModel PhotosDesignTimeVM
        {
            get { return new PhotosViewModel("design", 0); }
        }

        /// <summary>
        /// Возвращает экземпляр модели представления страницы картинок времени разработки.
        /// </summary>
        public ImageViewModel ImageDesignTimeVM
        {
            get
            {
                return new ImageViewModel("design", new Tuple<List<VKPhotoExtended>, int>(
                    new List<VKPhotoExtended> { DesignDataHelper.GetPhoto(), DesignDataHelper.GetPhoto(), DesignDataHelper.GetPhoto() }, 
                    2));
            }
        }
#endif
    }
}