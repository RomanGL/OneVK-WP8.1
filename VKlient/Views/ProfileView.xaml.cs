using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using OneVK.Enums.App;
using OneVK.Helpers;
using OneVK.ViewModel;
using OneVK.ViewModel.Messages;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using OneVK.Model.Profile;
using OneVK.Model.Group;
using Windows.UI.Xaml;
using OneVK.Controls;
using Windows.UI.Xaml.Input;

namespace OneVK.Views
{
    /// <summary>
    /// Представляет страницу профиля пользователя.
    /// </summary>
    public sealed partial class ProfileView : Page
    {
        ulong userID;

        public ProfileView()
        {
            InitializeComponent();
        }

        private ProfileViewModel vm;
        private ScrollViewer wallScrollViewer;
           
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            userID = (ulong)e.Parameter;
            string uniqueKey = CoreHelper.GetProfileViewModelKey(userID);

            vm = ServiceLocator.Current.GetInstance<KeyedViewModelLocator>()
                .GetByKey(uniqueKey, () => new ProfileViewModel(uniqueKey, userID));
            vm.Activate();
            DataContext = vm;

            if (e.NavigationMode == NavigationMode.New)
                vm.WallScrollOffset = 0;

            WallList.Loaded += WallList_Loaded;            
        }
        
        private void WallList_Loaded(object sender, RoutedEventArgs e)
        {
            wallScrollViewer = WallList.GetListViewScrollViewer();
            wallScrollViewer.ViewChanging += (s, args) =>
            {
                if (args.FinalView.VerticalOffset > 150)
                    ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.IntermediateFull);
                else
                    ChromeFrame.SetIsVisible(this, ChromeFrame.VisibilityStates.Intermediate);
            };

            //wallScrollViewer.ChangeView(null, vm.WallScrollOffset, null, true);
            WallList.Loaded -= WallList_Loaded;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //    vm.DeleteInstance();
            //vm.WallScrollOffset = wallScrollViewer.VerticalOffset;
        }

        private void FriendsGroupsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ulong parameter;
            AppViews page;
            if (e.ClickedItem is VKProfileShort)
	        {
                var profile = (VKProfileShort)e.ClickedItem;
                if (profile.ID < 0) return;

                parameter = (ulong)((VKProfileShort)e.ClickedItem).ID;
                page = AppViews.ProfileView;
	        }
            else
            {
                parameter = ((VKGroupShort)e.ClickedItem).ID;
                page = AppViews.GroupInfoView;
            }
            Messenger.Default.Send(new NavigateToPageMessage
            {
                Page = page,
                Operation = NavigationType.New,
                Parameter = parameter
            });
        }

        private void VideoCounter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigationHelper.Navigate(AppViews.VideosView, (long)userID);
        }

        private void AudioCounter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NavigationHelper.Navigate(AppViews.AudiosView, (long)userID);
        }
    }
}
