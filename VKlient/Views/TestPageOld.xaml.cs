using GalaSoft.MvvmLight.Command;
using OneVK.Enums.LongPoll;
using OneVK.Enums.Message;
using OneVK.Helpers;
using OneVK.Model.LongPoll;
using OneVK.Model.Message;
using OneVK.Request;
using OneVK.Service;
using OneVK.Controls;
using OneVK.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Storage;
using GalaSoft.MvvmLight.Messaging;
using OneVK.ViewModel.Messages;
using OneVK.Enums.App;
using NotificationsExtensions.TileContent;
using NotificationsExtensions.BadgeContent;
using NotificationsExtensions.ToastContent;
using Windows.UI.Notifications;
using VKSaver.Commands.SDK;

namespace OneVK.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class TestPageOld : Page
    {
        public TestPageOld()
        {
            InitializeComponent();

            Logout = new RelayCommand(() => VKHelper.Reset());
            GetUniqueDeviceID = new RelayCommand(async () => await(new MessageDialog(CoreHelper.GetUniqueDeviceID(), "UniqueDeviceID")).ShowAsync());
            GetDeviceName = new RelayCommand(async () => await (new MessageDialog(CoreHelper.GetDeviceName(), "DeviceName")).ShowAsync());
            GetOperatingSystemName = new RelayCommand(async () => await (new MessageDialog(CoreHelper.GetOperatingSystemName(), "OperatingSystem")).ShowAsync());
            CaptchaForce = new RelayCommand(async () => await (new CaptchaForceRequest()).ExecuteAsync());
            ShowLocalFolderPath = new RelayCommand(async () => await (new MessageDialog(ApplicationData.Current.LocalFolder.Path, "LocalFolder Path")).ShowAsync());
            TurnOnNotification = new RelayCommand(async () => 
                {
                    bool result = await NotificationsHelper.Connect();
                    await (new MessageDialog(result ? "Success" : "Fail", "Push notifications")).ShowAsync();
                });

            TestMessageFlags = new RelayCommand(async () =>
            {
                var flags = (VKMessageFlags)243;
                await (new MessageDialog(flags.ToString(), "243 as VKMessageFlags")).ShowAsync();
                flags = (VKMessageFlags)241;
                await(new MessageDialog(flags.ToString(), "241 as VKMessageFlags")).ShowAsync();
            });
            StartLongPolling = new RelayCommand(() => 
                {
                    ServiceHelper.VKLongPollService.StartLongPolling();
                    StopLongPolling.RaiseCanExecuteChanged();
                    StartLongPolling.RaiseCanExecuteChanged();
                }, () => true);
            StopLongPolling = new RelayCommand(() => 
                {
                    ServiceHelper.VKLongPollService.StopLongPolling();
                    StartLongPolling.RaiseCanExecuteChanged();
                    StopLongPolling.RaiseCanExecuteChanged();
                }, () => true);
            
            ShowToast = new RelayCommand(() =>
            {
                ((ChromeFrame)Frame).ShowPopup(new PopupMessage
                {
                    Title = "Добро пожаловать в OneVK", Content = "Это уведомление вернет вас на тестовую страницу",
                    Parameter = new NavigateToPageMessage() { Page = AppViews.TestView },
                    Type = PopupMessageType.Info                    
                });
            });

            GoToBotView = new RelayCommand(() =>
            {
                Messenger.Default.Send<NavigateToPageMessage>(new NavigateToPageMessage
                {
                    Page = AppViews.BotView,
                    Operation = NavigationType.New
                }); 
            });
            GoToBlankPage = new RelayCommand(() => Frame.Navigate(typeof(BlankPage1)));

            ClearBadgeTile = new RelayCommand(() =>
            {
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Clear();
            });

            SendBadgeTile = new RelayCommand(() =>
            {
                var badge = new BadgeNumericNotificationContent(7).CreateNotification();
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
            });

            SendMessageTile = new RelayCommand(() =>
            {
                var tile = TileContentFactory.CreateTileSquare150x150IconWithBadge();
                tile.ImageIcon.Src = "ms-appx:///Assets/BadgeIcon.png";
                TileUpdateManager.CreateTileUpdaterForApplication().Update(tile.CreateNotification());

                var badge = new BadgeNumericNotificationContent(7).CreateNotification();
                BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
            });

            SendToast = new RelayCommand(() =>
            {
                var toast = ToastContentFactory.CreateToastText02();
                toast.Audio.Content = ToastAudioContent.IM;
                toast.Duration = ToastDuration.Long;
                toast.TextHeading.Text = "OneVK";
                toast.TextBodyWrap.Text = "Это тестовое уведомление";

                ToastNotificationManager.CreateToastNotifier().Show(toast.CreateNotification());
            });

            StartVKSaver = new RelayCommand(async () =>
            {
                IVKSaverCommand command = new VKSaverStartAppCommand();
                await command.TryExecute();
            });
        }

        public RelayCommand Logout { get; private set; }
        public RelayCommand GoToBotView { get; private set; }
        public RelayCommand GoToBlankPage{ get; private set; }
        public RelayCommand GetUniqueDeviceID { get; private set; }
        public RelayCommand GetDeviceName { get; private set; }
        public RelayCommand GetOperatingSystemName { get; private set; }
        public RelayCommand CaptchaForce { get; private set; }
        public RelayCommand TestMessageFlags { get; private set; }
        public RelayCommand TurnOnNotification { get; private set; }
        public RelayCommand StartLongPolling { get; private set; }
        public RelayCommand StopLongPolling { get; private set; }
        public RelayCommand ShowToast { get; private set; }
        public RelayCommand ShowLocalFolderPath { get; private set; }

        public RelayCommand SendBadgeTile { get; private set; }
        public RelayCommand ClearBadgeTile { get; private set; }
        public RelayCommand SendMessageTile { get; private set; }
        public RelayCommand ClearMessageTile { get; private set; }
        public RelayCommand SendToast { get; private set; }

        public RelayCommand StartVKSaver { get; private set; }
    }
}
