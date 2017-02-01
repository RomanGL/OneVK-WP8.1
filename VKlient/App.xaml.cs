using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using OneVK.Controls;
using OneVK.Enums.App;
using OneVK.Response;
using OneVK.Views;
using OneVK.ViewModel.Messages;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.StoreApps;
using Microsoft.Practices.Unity;
using OneVK.Core.VK;
using OneVK.Core.Services;
using System.Globalization;
using OneVK.Core.Models.AppNotifications;
using OneVK.Core.VK.Models.Groups;
using OneVK.Core.ViewModels.Collections;
using OneVK.Core.VK.Models.Newsfeed;
using OneVK.Core.VK.Models.Users;

namespace OneVK
{
    /// <summary>
    /// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
    /// </summary>
    public sealed partial class App : MvvmAppBase
    {
        private const string ENABLE_PUSH_NOTIFICATIONS = "EnablePushNotifications";
        private const string VIEW_MODEL_FORMAT = "OneVK.Core.ViewModels.{0}Model, OneVK.Core.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string VIEW_MODEL_CONTROLS_FORMAT = "OneVK.Core.ViewModels.{0}ViewModel, OneVK.Core.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        private const string VIEW_FORMAT = "OneVK.Views.{0}";

        private IUnityContainer container;    
        private ChromeFrame rootVisual;

        public static bool CancelGoBack { get; set; }

        public IUnityContainer Container { get { return container; } }

        public App()
        {            
            InitializeComponent();
            UnhandledException += App_UnhandledException;

            FrameFactory = () =>
            {
                rootVisual = new ChromeFrame();
                return rootVisual;
            };
        }

        private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var data = e.Exception.ToString();
            string name = e.Exception.GetType().ToString();

            var notification = new AppNotification
            {
                Type = AppNotificationType.Error,
                Title = $"Произошла ошибка\n{name}",
                Content = "Коснитесь для подробностей",
                DestinationView = "ErrorView",
                NavigationParameter = JsonConvert.SerializeObject(new Tuple<string, string>(name, data)),
                Duration = TimeSpan.FromSeconds(9)
            };
            container.Resolve<IAppNotificationsService>().SendNotification(notification);
            
            e.Handled = true;
        }

        /// <summary>
        /// Вызывается при обычном запуске приложения пользователем.  Будут использоваться другие точки входа,
        /// если приложение запускается для открытия конкретного файла, отображения
        /// результатов поиска и т. д.
        /// </summary>
        /// <param name="e">Сведения о запросе и обработке запуска.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Helpers.CoreHelper.Dispatcher = Window.Current.Dispatcher;

            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            StatusBar.GetForCurrentView().ForegroundColor = Colors.White;
            StatusBar.GetForCurrentView().BackgroundColor = Color.FromArgb(255, 0, 0, 0);
            StatusBar.GetForCurrentView().BackgroundOpacity = 0.2;

            base.OnLaunched(e);
        }

        protected override void OnInitialize(IActivatedEventArgs args)
        {
            container = new UnityContainer();
            ViewModelLocator.SetDefaultViewTypeToViewModelTypeResolver(GetViewModelType);

            container.RegisterInstance(NavigationService);
            container.RegisterInstance(SessionStateService);
            container.RegisterInstance<IAppNotificationsPresenter>(rootVisual);
            container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAppNotificationsService, AppNotificationsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IVKLongPollService, VKLongPollService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IVKLoginService, VKLoginService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IVKService, VKService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGrooveMusicService, GrooveMusicService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IImagesCacheService, ImagesCacheService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISoundService, SoundService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeviceVibrationService, DeviceVibrationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeviceInformationService, DeviceInformationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPushNotificationsService, VKPushNotificationsService>(new ContainerControlledLifetimeManager());

            SimpleIoc.Default.Register<GalaSoft.MvvmLight.Views.IDialogService, GalaSoft.MvvmLight.Views.DialogService>();
            SimpleIoc.Default.Register<Service.IPlayerService, Service.PlayerService>();

            InitializeServices();

            var vkLoginService = container.Resolve<IVKLoginService>();
            var vkLongPollService = container.Resolve<IVKLongPollService>();

            vkLoginService.UserLogin += (s, e) =>
            {
                Helpers.ServiceHelper.VKLongPollService.StartLongPolling(true);
                if (container.Resolve<ISettingsService>().Get(ENABLE_PUSH_NOTIFICATIONS, true))
                    UpdatePushState(true);
                //vkLongPollService.Start();
            };
            vkLoginService.UserLogout += (s, e) =>
            {
                Helpers.ServiceHelper.VKLongPollService.StopLongPolling();
                if (container.Resolve<ISettingsService>().Get(ENABLE_PUSH_NOTIFICATIONS, true))
                    UpdatePushState(false);
                //vkLongPollService.Stop();
            };

            if (vkLoginService.IsAuthorized)
            {
                Helpers.ServiceHelper.VKLongPollService.StartLongPolling(true);
                //vkLongPollService.Start();
            }

            base.OnInitialize(args);
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            SessionStateService.RegisterKnownType(typeof(VKGroup));
            SessionStateService.RegisterKnownType(typeof(VKGroupExtended));
            SessionStateService.RegisterKnownType(typeof(VKGroupSettings));
            SessionStateService.RegisterKnownType(typeof(VKUser));
            SessionStateService.RegisterKnownType(typeof(PaginatedCollection<VKNewsfeedItem>));

            base.OnRegisterKnownTypesForSerialization();
        }

        /// <summary>
        /// Возвращает тип страницы по ее имени.
        /// </summary>
        /// <param name="view">Название представления.</param>
        private Type GetPageTypeFromName(AppViews view)
        {
            switch (view)
            {
                case AppViews.LoginView:
                    return typeof(LoginView);
                case AppViews.NewsView:
                    return typeof(NewsView);
                case AppViews.FriendsView:
                    return typeof(FriendsView);
                case AppViews.ErrorView:
                    return typeof(ErrorView);
                case AppViews.PostView:
                    return typeof(PostView);
                case AppViews.ImageView:
                    return typeof(ImageView);
                case AppViews.AudiosView:
                    return typeof(AudiosView);
                case AppViews.VideosView:
                    return typeof(VideosView);
                case AppViews.VideoInfoView:
                    return typeof(VideoInfoView);
                case AppViews.VideoAlbumView:
                    return typeof(VideoAlbumView);
                case AppViews.GroupsView:
                    return typeof(GroupsView);
                case AppViews.MessagesView:
                    return typeof(MessagesView);
                case AppViews.TestView:
                    return typeof(TestPageOld);
                case AppViews.BotView:
                    return typeof(BotView);
                case AppViews.GroupInfoView:
                    return typeof(GroupInfoView);
                case AppViews.ProfileView:
                    return typeof(ProfileView);
                case AppViews.SettingsView:
                    return typeof(SettingsView);
                case AppViews.ChangeStatusView:
                    return typeof(ChangeStatusView);
                case AppViews.PhotosView:
                    return typeof(PhotosView);
                case AppViews.AccessDeniedView:
                    return typeof(AccessDeniedView);
                case AppViews.PlayerView:
                    return typeof(PlayerView);
                case AppViews.NewPostView:
                    return typeof(NewPostView);
                case AppViews.NotificationsSettingsView:
                    return typeof(NotificationsSettingsView);
                case AppViews.CommonSettingsView:
                    return typeof(CommonSettigsView);
                case AppViews.ConversationView:
                    return typeof(ConversationView);
                case AppViews.AboutView:
                    return typeof(AboutView);
                case AppViews.ChatSettingsView:
                    return typeof(ChatSettingsView);
                case AppViews.ChoiceFriendsView:
                    return typeof(ChoiceFriendsView);
                case AppViews.FeedbackView:
                    return typeof(FeedbackView);
                case AppViews.PromoView:
                    return typeof(PromoView);
                case AppViews.AllPromosView:
                    return typeof(AllPromosView);
                case AppViews.AttachmentsManagerView:
                    return typeof(AttachmentsManagerView);
            }
            return typeof(ErrorView);
        }
        
        private bool _isInitialized;
        private void InitializeServices()
        {
            if (!_isInitialized)
            {
                Helpers.CoreHelper.LockOrientation();

                Helpers.VKHelper.CaptchaHandler = async (request, callback) =>
                {
                    await rootVisual.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        async () =>
                        {
                            var captcha = new EnterCaptcha(request.CaptchaURL);
                            if (await captcha.ShowAsync() == ContentDialogResult.Primary)
                                callback(new VKCaptchaResponse(request) { IsCanceled = false, UserResponse = captcha.Captcha });
                            else
                                callback(new VKCaptchaResponse(request) { IsCanceled = true, UserResponse = "" });
                        });
                };                

                Messenger.Default.Register<NavigateToPageMessage>(this, async message =>
                {
                    await rootVisual.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        rootVisual.CommonState = ChromeFrame.CommonStates.Normal;
                        if (message.Operation == NavigationType.New)
                        {
                            var targerType = GetPageTypeFromName(message.Page);
                            if (targerType == rootVisual.CurrentSourcePageType && message.Parameter == null)
                                return;
                            NavigationService.Navigate(targerType.Name, message.Parameter);
                        }
                        else if (message.Operation == NavigationType.GoBack && rootVisual.CanGoBack)
                            NavigationService.GoBack();
                        else if (message.Operation == NavigationType.GoForward && rootVisual.CanGoForward)
                            NavigationService.GoForward();
                        else if (message.Operation == NavigationType.NewClear)
                        {
                            var targerType = GetPageTypeFromName(message.Page);
                            if (targerType == rootVisual.CurrentSourcePageType && message.Parameter == null)
                                return;
                            NavigationService.Navigate(targerType.Name, message.Parameter);
                            NavigationService.ClearHistory();
                        }                        
                    });
                });

                HardwareButtons.BackPressed += (s, args) =>
                {
                    if (CancelGoBack) return;
                    if (rootVisual.CommonState == ChromeFrame.CommonStates.MenuOpened)
                    {
                        rootVisual.CommonState = ChromeFrame.CommonStates.Normal;
                        args.Handled = true;
                    }
                    else if (rootVisual.CanGoBack)
                    {
                        args.Handled = true;
                        rootVisual.GoBack();
                    }
                };

                Helpers.ServiceHelper.PlayerService.Initialize();
                _isInitialized = true;
            }
        }

        protected override Task OnLaunchApplication(LaunchActivatedEventArgs args)
        {    
            if (container.Resolve<IVKLoginService>().IsAuthorized)
            {
                if (!String.IsNullOrEmpty(args.Arguments))
                {

                }
                else if (args.PreviousExecutionState == ApplicationExecutionState.NotRunning ||
                    args.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
                {
                    if (container.Resolve<ISettingsService>().Get(ENABLE_PUSH_NOTIFICATIONS, true))
                        UpdatePushState(true);
                    NavigationService.Navigate("NewsfeedView", null);
                }                    
            }
            else
            {
                NavigationService.Navigate("LoginView", null);
            }

            Window.Current.Activate();
            return Task.FromResult<object>(null);
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
        }

        /// <summary>
        /// Возвращает тип модели представления для типа представления.
        /// </summary>
        /// <param name="viewType">Тип представления.</param>
        private Type GetViewModelType(Type viewType)
        {
            string viewModelTypeName = null;
            if (viewType.Name.EndsWith("View"))
                viewModelTypeName = String.Format(CultureInfo.InvariantCulture, VIEW_MODEL_FORMAT, viewType.Name);
            else
                viewModelTypeName = String.Format(CultureInfo.InvariantCulture, VIEW_MODEL_CONTROLS_FORMAT, viewType.Name);

            return Type.GetType(viewModelTypeName);
        }

        private async void UpdatePushState(bool isOn)
        {
            var service = container.Resolve<IPushNotificationsService>();
            byte currentRetry = 0;
            while (currentRetry < 4)
            {
                try
                {
                    if (isOn) await service.RegisterDevice();
                    else await service.UnregisterDevice();
                    break;
                }
                catch (Exception)
                {
                    if (++currentRetry < 4)
                    {
                        int timeout = currentRetry * 5;
                        await Task.Delay(timeout * 1000);
                    }
                }
            }
        }

        protected override Type GetPageType(string pageToken)
        {
            return Type.GetType(String.Format(CultureInfo.InvariantCulture, VIEW_FORMAT, pageToken));
        }

        protected override object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}